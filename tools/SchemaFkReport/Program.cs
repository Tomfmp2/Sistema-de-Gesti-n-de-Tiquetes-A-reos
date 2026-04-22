using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

static string Normalize(string? s) => (s ?? string.Empty).Trim().ToLowerInvariant();

using var ctx = DbContextFactory.Create();

var argsList = Environment.GetCommandLineArgs().Skip(1).ToArray();
var tableArg = argsList.FirstOrDefault(a => a.StartsWith("--table=", StringComparison.OrdinalIgnoreCase));
var fksArg = argsList.FirstOrDefault(a => a.StartsWith("--fks=", StringComparison.OrdinalIgnoreCase));
var tablesArg = argsList.Any(a => a.Equals("--tables", StringComparison.OrdinalIgnoreCase));
var historyArg = argsList.Any(a => a.Equals("--history", StringComparison.OrdinalIgnoreCase));

if (tablesArg)
{
    var tSql = """
SELECT TABLE_NAME AS TableName
FROM information_schema.TABLES
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_TYPE = 'BASE TABLE'
ORDER BY TABLE_NAME;
""";

    var tables = await ctx.Database.SqlQueryRaw<DbTableRow>(tSql).ToListAsync();
    Console.WriteLine($"DB: {ctx.Database.GetDbConnection().Database}");
    Console.WriteLine($"Tables ({tables.Count}):");
    foreach (var t in tables) Console.WriteLine($"- {t.TableName}");
    return;
}

if (historyArg)
{
    var hSql = "SELECT MigrationId, ProductVersion FROM __EFMigrationsHistory ORDER BY MigrationId;";
    var rows = await ctx.Database.SqlQueryRaw<DbHistoryRow>(hSql).ToListAsync();
    Console.WriteLine($"DB: {ctx.Database.GetDbConnection().Database}");
    Console.WriteLine("__EFMigrationsHistory:");
    foreach (var r in rows) Console.WriteLine($"- {r.MigrationId} | {r.ProductVersion}");
    return;
}
if (!string.IsNullOrWhiteSpace(tableArg))
{
    var tableName = Normalize(tableArg.Split('=', 2)[1]);
    var colSql = """
SELECT COLUMN_NAME AS ColumnName
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_NAME = {0}
ORDER BY ORDINAL_POSITION;
""";

    var cols = await ctx.Database.SqlQueryRaw<DbColumnRow>(colSql, tableName).ToListAsync();
    Console.WriteLine($"DB: {ctx.Database.GetDbConnection().Database}");
    Console.WriteLine($"Table `{tableName}` columns ({cols.Count}):");
    foreach (var c in cols) Console.WriteLine($"- {c.ColumnName}");
    return;
}

if (!string.IsNullOrWhiteSpace(fksArg))
{
    var tableName = Normalize(fksArg.Split('=', 2)[1]);
    var fkSql = """
SELECT
  k.CONSTRAINT_NAME AS ConstraintName,
  k.TABLE_NAME AS DepTable,
  k.COLUMN_NAME AS DepCol,
  k.REFERENCED_TABLE_NAME AS PrinTable,
  k.REFERENCED_COLUMN_NAME AS PrinCol
FROM information_schema.KEY_COLUMN_USAGE k
JOIN information_schema.TABLE_CONSTRAINTS c
  ON c.CONSTRAINT_SCHEMA = k.CONSTRAINT_SCHEMA
 AND c.TABLE_NAME = k.TABLE_NAME
 AND c.CONSTRAINT_NAME = k.CONSTRAINT_NAME
WHERE k.CONSTRAINT_SCHEMA = DATABASE()
  AND c.CONSTRAINT_TYPE = 'FOREIGN KEY'
  AND k.TABLE_NAME = {0}
ORDER BY k.CONSTRAINT_NAME, k.ORDINAL_POSITION;
""";

    var fks = await ctx.Database.SqlQueryRaw<DbFkRow>(fkSql, tableName).ToListAsync();
    Console.WriteLine($"DB: {ctx.Database.GetDbConnection().Database}");
    Console.WriteLine($"Table `{tableName}` foreign keys ({fks.Count} columns):");
    foreach (var fk in fks)
        Console.WriteLine($"- {fk.ConstraintName}: {fk.DepTable}.{fk.DepCol} -> {fk.PrinTable}.{fk.PrinCol}");
    return;
}

var efFks = ctx.Model
    .GetEntityTypes()
    .SelectMany(et => et.GetForeignKeys().Select(fk => (EntityType: et, Fk: fk)))
    .Select(x =>
    {
        var depTable = x.EntityType.GetTableName() ?? x.EntityType.GetDefaultTableName();
        var depSchema = x.EntityType.GetSchema();
        var principalEntityType = x.Fk.PrincipalEntityType;
        var prinTable = principalEntityType.GetTableName() ?? principalEntityType.GetDefaultTableName();
        var prinSchema = principalEntityType.GetSchema();

        var depCols = x.Fk.Properties.Select(p => p.GetColumnName(StoreObjectIdentifier.Table(depTable!, depSchema))!).ToArray();
        var prinCols = x.Fk.PrincipalKey.Properties.Select(p => p.GetColumnName(StoreObjectIdentifier.Table(prinTable!, prinSchema))!).ToArray();

        return new
        {
            DepTable = Normalize(depTable),
            DepCols = depCols.Select(Normalize).ToArray(),
            PrinTable = Normalize(prinTable),
            PrinCols = prinCols.Select(Normalize).ToArray(),
        };
    })
    .Where(x => !string.IsNullOrWhiteSpace(x.DepTable) && !string.IsNullOrWhiteSpace(x.PrinTable))
    .ToList();

// Read FK constraints from MySQL information_schema (current DB)
var sql = """
SELECT
  k.TABLE_NAME AS DepTable,
  k.COLUMN_NAME AS DepCol,
  k.REFERENCED_TABLE_NAME AS PrinTable,
  k.REFERENCED_COLUMN_NAME AS PrinCol,
  k.CONSTRAINT_NAME AS ConstraintName
FROM information_schema.KEY_COLUMN_USAGE k
JOIN information_schema.TABLE_CONSTRAINTS c
  ON c.CONSTRAINT_SCHEMA = k.CONSTRAINT_SCHEMA
 AND c.TABLE_NAME = k.TABLE_NAME
 AND c.CONSTRAINT_NAME = k.CONSTRAINT_NAME
WHERE k.CONSTRAINT_SCHEMA = DATABASE()
  AND c.CONSTRAINT_TYPE = 'FOREIGN KEY'
  AND k.REFERENCED_TABLE_NAME IS NOT NULL
ORDER BY k.TABLE_NAME, k.CONSTRAINT_NAME, k.ORDINAL_POSITION;
""";

var dbFks = await ctx.Database
    .SqlQueryRaw<DbFkRow>(sql)
    .ToListAsync();

var dbFkSet = dbFks
    .Select(r => (DepTable: Normalize(r.DepTable), DepCol: Normalize(r.DepCol), PrinTable: Normalize(r.PrinTable), PrinCol: Normalize(r.PrinCol)))
    .ToHashSet();

var missing = new List<string>();

foreach (var fk in efFks)
{
    // Compare column-by-column (covers composite keys too)
    for (var i = 0; i < Math.Min(fk.DepCols.Length, fk.PrinCols.Length); i++)
    {
        var key = (fk.DepTable, fk.DepCols[i], fk.PrinTable, fk.PrinCols[i]);
        if (!dbFkSet.Contains(key))
        {
            missing.Add($"{fk.DepTable}.{fk.DepCols[i]} -> {fk.PrinTable}.{fk.PrinCols[i]}");
        }
    }
}

Console.WriteLine($"DB: {ctx.Database.GetDbConnection().Database}");
Console.WriteLine($"EF FKs: {efFks.Count} | DB FK columns: {dbFkSet.Count}");
Console.WriteLine();

if (missing.Count == 0)
{
    Console.WriteLine("OK: No missing foreign keys found in DB compared to EF model.");
    return;
}

Console.WriteLine("Missing FKs in DB (present in EF model, not present as MySQL FK constraint):");
foreach (var line in missing.Distinct().OrderBy(x => x))
{
    Console.WriteLine($"- {line}");
}

public sealed class DbFkRow
{
    public string DepTable { get; init; } = "";
    public string DepCol { get; init; } = "";
    public string PrinTable { get; init; } = "";
    public string PrinCol { get; init; } = "";
    public string ConstraintName { get; init; } = "";
}

public sealed class DbColumnRow
{
    public string ColumnName { get; init; } = "";
}

public sealed class DbTableRow
{
    public string TableName { get; init; } = "";
}

public sealed class DbHistoryRow
{
    public string MigrationId { get; init; } = "";
    public string ProductVersion { get; init; } = "";
}


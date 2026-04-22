using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

using var ctx = DbContextFactory.Create();

static string Normalize(string s) => s.Trim().ToLowerInvariant();
static string MakeName(string depTable, string depCol, string prinTable) =>
    $"FK_{depTable}_{depCol}_{prinTable}".Replace("-", "_");

// Pull missing FK list using the same logic as SchemaFkReport output (EF model vs DB constraints)
var efMissing = await GetMissingEfFkPairs(ctx);

Console.WriteLine($"DB: {ctx.Database.GetDbConnection().Database}");
Console.WriteLine($"Missing FK column-pairs to create: {efMissing.Count}");

var created = 0;
var skipped = 0;

foreach (var fk in efMissing.OrderBy(x => x.DepTable).ThenBy(x => x.DepCol))
{
    var depCol = fk.DepCol;
    var prinCol = fk.PrinCol;

    // Small compatibility layer for DBs that still use legacy column names
    if (!await TableHasColumn(ctx, fk.DepTable, depCol))
    {
        if (fk.DepTable == "invoice_items" && fk.DepCol == "booking_passenger_id" && await TableHasColumn(ctx, fk.DepTable, "reservation_passenger_id"))
            depCol = "reservation_passenger_id";
        else if (fk.DepTable == "persons" && fk.DepCol == "document_type_id" && await TableHasColumn(ctx, fk.DepTable, "DocumentTypeId"))
            depCol = "DocumentTypeId";
        else if (fk.DepTable == "route_stopovers" && fk.DepCol == "stopover_airport_id" && await TableHasColumn(ctx, fk.DepTable, "layover_airport_id"))
            depCol = "layover_airport_id";
        else
        {
            Console.WriteLine($"SKIP (no column): {fk.DepTable}.{fk.DepCol}");
            skipped++;
            continue;
        }
    }
    if (!await TableHasColumn(ctx, fk.PrinTable, fk.PrinCol))
    {
        Console.WriteLine($"SKIP (no principal column): {fk.PrinTable}.{fk.PrinCol}");
        skipped++;
        continue;
    }

    if (await DbHasFk(ctx, fk.DepTable, depCol, fk.PrinTable, prinCol))
    {
        Console.WriteLine($"OK (exists): {fk.DepTable}.{depCol} -> {fk.PrinTable}.{prinCol}");
        continue;
    }

    var baseName = MakeName(fk.DepTable, depCol, fk.PrinTable);
    var name = baseName;
    var i = 0;
    while (await DbHasConstraintName(ctx, name))
    {
        i++;
        name = $"{baseName}_{i}";
    }

    var sql =
        $"ALTER TABLE `{fk.DepTable}` ADD CONSTRAINT `{name}` FOREIGN KEY (`{depCol}`) REFERENCES `{fk.PrinTable}` (`{prinCol}`) ON DELETE RESTRICT;";

    try
    {
        await ctx.Database.ExecuteSqlRawAsync(sql);
        Console.WriteLine($"ADD: {name}: {fk.DepTable}.{depCol} -> {fk.PrinTable}.{prinCol}");
        created++;
    }
    catch (Exception ex)
    {
        Console.WriteLine($"FAIL: {fk.DepTable}.{depCol} -> {fk.PrinTable}.{prinCol} | {ex.GetType().Name}: {ex.Message}");
        skipped++;
    }
}

Console.WriteLine();
Console.WriteLine($"Created: {created} | Skipped/Failed: {skipped}");

static async Task<List<FkPair>> GetMissingEfFkPairs(DbContext ctx)
{
    static string N(string? s) => Normalize(s ?? "");

    var efFks = ctx.Model
        .GetEntityTypes()
        .SelectMany(et => et.GetForeignKeys().Select(fk => (EntityType: et, Fk: fk)))
        .Select(x =>
        {
            var depTable = x.EntityType.GetTableName() ?? x.EntityType.GetDefaultTableName();
            var depSchema = x.EntityType.GetSchema();
            var prinEt = x.Fk.PrincipalEntityType;
            var prinTable = prinEt.GetTableName() ?? prinEt.GetDefaultTableName();
            var prinSchema = prinEt.GetSchema();

            var depCols = x.Fk.Properties
                .Select(p => p.GetColumnName(Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(depTable!, depSchema))!)
                .ToArray();
            var prinCols = x.Fk.PrincipalKey.Properties
                .Select(p => p.GetColumnName(Microsoft.EntityFrameworkCore.Metadata.StoreObjectIdentifier.Table(prinTable!, prinSchema))!)
                .ToArray();

            return (DepTable: N(depTable), DepCols: depCols.Select(N).ToArray(), PrinTable: N(prinTable), PrinCols: prinCols.Select(N).ToArray());
        })
        .ToList();

    var dbFkSql = """
SELECT
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
  AND k.REFERENCED_TABLE_NAME IS NOT NULL;
""";

    var dbFks = await ctx.Database.SqlQueryRaw<DbFk>(dbFkSql).ToListAsync();
    var dbSet = dbFks.Select(r => (N(r.DepTable), N(r.DepCol), N(r.PrinTable), N(r.PrinCol))).ToHashSet();

    var missing = new List<FkPair>();
    foreach (var fk in efFks)
    {
        for (var i = 0; i < Math.Min(fk.DepCols.Length, fk.PrinCols.Length); i++)
        {
            var key = (fk.DepTable, fk.DepCols[i], fk.PrinTable, fk.PrinCols[i]);
            if (!dbSet.Contains(key))
                missing.Add(new FkPair(fk.DepTable, fk.DepCols[i], fk.PrinTable, fk.PrinCols[i]));
        }
    }

    return missing.Distinct().ToList();
}

static async Task<bool> TableHasColumn(DbContext ctx, string table, string col)
{
    var sql = """
SELECT COUNT(*) AS Cnt
FROM information_schema.COLUMNS
WHERE TABLE_SCHEMA = DATABASE()
  AND TABLE_NAME = {0}
  AND COLUMN_NAME = {1};
""";

    var cnt = await ctx.Database.SqlQueryRaw<ScalarInt>(sql, table, col).ToListAsync();
    return cnt.FirstOrDefault()?.Cnt > 0;
}

static async Task<bool> DbHasFk(DbContext ctx, string depTable, string depCol, string prinTable, string prinCol)
{
    var sql = """
SELECT COUNT(*) AS Cnt
FROM information_schema.KEY_COLUMN_USAGE k
JOIN information_schema.TABLE_CONSTRAINTS c
  ON c.CONSTRAINT_SCHEMA = k.CONSTRAINT_SCHEMA
 AND c.TABLE_NAME = k.TABLE_NAME
 AND c.CONSTRAINT_NAME = k.CONSTRAINT_NAME
WHERE k.CONSTRAINT_SCHEMA = DATABASE()
  AND c.CONSTRAINT_TYPE = 'FOREIGN KEY'
  AND k.TABLE_NAME = {0}
  AND k.COLUMN_NAME = {1}
  AND k.REFERENCED_TABLE_NAME = {2}
  AND k.REFERENCED_COLUMN_NAME = {3};
""";

    var cnt = await ctx.Database.SqlQueryRaw<ScalarInt>(sql, depTable, depCol, prinTable, prinCol).ToListAsync();
    return cnt.FirstOrDefault()?.Cnt > 0;
}

static async Task<bool> DbHasConstraintName(DbContext ctx, string constraintName)
{
    var sql = """
SELECT COUNT(*) AS Cnt
FROM information_schema.TABLE_CONSTRAINTS
WHERE CONSTRAINT_SCHEMA = DATABASE()
  AND CONSTRAINT_NAME = {0};
""";

    var cnt = await ctx.Database.SqlQueryRaw<ScalarInt>(sql, constraintName).ToListAsync();
    return cnt.FirstOrDefault()?.Cnt > 0;
}

public sealed record FkPair(string DepTable, string DepCol, string PrinTable, string PrinCol);

public sealed class DbFk
{
    public string DepTable { get; init; } = "";
    public string DepCol { get; init; } = "";
    public string PrinTable { get; init; } = "";
    public string PrinCol { get; init; } = "";
}

public sealed class ScalarInt
{
    public int Cnt { get; init; }
}


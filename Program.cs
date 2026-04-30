using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RolePermissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Data;
using ContinentCatalogSeed = sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Seed.CatalogSeed;
using CountryCatalogSeed = sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Seed.CatalogSeed;

try
{
    SpectreUi.InitializeConsoleUi();

    var context = DbContextFactory.Create();

    if (!context.Database.CanConnect())
    {
        // Si el servidor MySQL está accesible pero la base de datos aún no existe,
        // intentamos crearla (CREATE DATABASE IF NOT EXISTS) y luego reintentamos.
        await TryCreateDatabaseIfMissingAsync();
        context = DbContextFactory.Create();
    }

    if (context.Database.CanConnect())
    {
        // Bootstrap automático (sin flags):
        // - Asegura que el esquema esté actualizado (migraciones)
        // - Carga catálogos mínimos para que el sistema funcione
        // - Garantiza usuario ROOT (admin) con permisos base
        //
        // Todo es idempotente: se puede ejecutar en cada arranque sin duplicar datos.
        context.Database.Migrate();
        Console.WriteLine("Conexión exitosa. Migraciones aplicadas.");

        await EnsureDefaultsAsync(context);
        await SeedRootUserAsync(context);

        var migrateRequested =
            args.Any(a => string.Equals(a, "--migrate", StringComparison.OrdinalIgnoreCase))
            || string.Equals(Environment.GetEnvironmentVariable("APPLY_MIGRATIONS"), "true", StringComparison.OrdinalIgnoreCase);

        if (migrateRequested)
        {
            context.Database.Migrate();
            Console.WriteLine("Migraciones aplicadas correctamente");
        }

        var describeArg = args.FirstOrDefault(a => a.StartsWith("--describe-table=", StringComparison.OrdinalIgnoreCase));
        if (describeArg is not null)
        {
            var table = describeArg.Split('=', 2)[1];
            await DescribeTableAsync(context, table);
            return;
        }

        if (args.Any(a => string.Equals(a, "--validate-mappings", StringComparison.OrdinalIgnoreCase)))
        {
            await ValidateMappingsAsync(context);
            return;
        }

        if (args.Any(a => string.Equals(a, "--seed-defaults", StringComparison.OrdinalIgnoreCase)))
        {
            // En este proyecto la DB puede venir de un SQL legacy, así que hacemos seed "seguro"
            // vía INSERTs idempotentes (sin depender de migraciones / nombres de FK).
            await EnsureDefaultsAsync(context);
            Console.WriteLine("Seed por defecto aplicado.");
            return;
        }

        if (args.Any(a => string.Equals(a, "--seed-root", StringComparison.OrdinalIgnoreCase)))
        {
            // Asegura que el catálogo mínimo exista antes de crear usuario ROOT.
            context.Database.Migrate();
            await SeedRootUserAsync(context);
            return;
        }

        if (args.Any(a => string.Equals(a, "--normalize-persons", StringComparison.OrdinalIgnoreCase)))
        {
            await NormalizePersonsColumnsAsync(context);
            Console.WriteLine("Normalización de `persons` completada.");
            return;
        }

        if (args.Any(a => string.Equals(a, "--debug-root", StringComparison.OrdinalIgnoreCase)))
        {
            await DebugRootAsync(context);
            return;
        }

        while (true)
        {
            var auth = await LoginShell.LoginAsync(context);
            var reason = await ApplicationShell.RunAsync(context, auth);
            if (reason is null)
            {
                break;
            }
        }
    }
    else
    {
        Console.WriteLine("No se pudo conectar a la base de datos.");
        Console.WriteLine("Verifica que MySQL esté instalado/encendido y que la cadena de conexión sea válida (MYSQL_CONNECTION o appsettings.json).");
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.Error.WriteLine($"Detalle: {ex.InnerException.Message}");
    }
}

static async Task TryCreateDatabaseIfMissingAsync()
{
    var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: true)
        .AddEnvironmentVariables()
        .Build();

    var cs =
        Environment.GetEnvironmentVariable("MYSQL_CONNECTION")
        ?? config.GetConnectionString("MySqlDB");

    if (string.IsNullOrWhiteSpace(cs))
        return;

    // Si falta el parámetro Database, no hay nada que crear automáticamente.
    var builder = new MySqlConnectionStringBuilder(cs);
    var dbName = builder.Database;
    if (string.IsNullOrWhiteSpace(dbName))
        return;

    // Conectamos al servidor sin especificar base de datos.
    builder.Database = string.Empty;

    try
    {
        await using var conn = new MySqlConnection(builder.ConnectionString);
        await conn.OpenAsync();
        await using var cmd = conn.CreateCommand();
        cmd.CommandText = $"CREATE DATABASE IF NOT EXISTS `{dbName}`;";
        await cmd.ExecuteNonQueryAsync();
    }
    catch
    {
        // Silencioso: si falla (servidor caído, credenciales, permisos),
        // el flujo principal mostrará el mensaje de conexión fallida.
    }
}

static async Task SeedRootUserAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context)
{
    const string username = "ROOT";
    const string passwordHash = "12345"; // LoginShell soporta password plano o SHA-256 hex

    // Preferimos el rol administrador “canónico” del seed (Id=1 / nombre Administrador),
    // pero mantenemos compatibilidad con BDs que aún usan "admin"/"ROOT".
    var role = await context.Set<SystemRoleEntity>()
        .OrderBy(r => r.Id)
        .FirstOrDefaultAsync(r => r.Id == 1)
        ?? await context.Set<SystemRoleEntity>()
            .OrderBy(r => r.Id)
            .FirstOrDefaultAsync(r =>
                r.Name == "Administrador"
                || r.Name == "ADMINISTRADOR"
                || r.Name == "admin"
                || r.Name == "ADMIN"
                || r.Name == "root"
                || r.Name == "ROOT");

    if (role is null)
    {
        role = new SystemRoleEntity { Name = "admin", Description = "Administrador del sistema" };
        context.Set<SystemRoleEntity>().Add(role);
        await context.SaveChangesAsync();
    }

    // Aseguramos permisos base y que el rol admin tenga TODOS los permisos.
    // (Si la BD fue creada por SQL legacy, esto evita quedar sin acceso.)
    var permissionNames = new[]
    {
        ("reservations.manage", "Gestionar reservas"),
        ("flights.manage", "Gestionar vuelos"),
        ("catalogs.manage", "Gestionar catálogos del sistema"),
        ("fares.manage", "Gestionar tarifas"),
        ("payments.manage", "Gestionar pagos"),
        ("tickets.manage", "Gestionar tickets"),
        ("checkins.manage", "Gestionar check-ins"),
        ("invoices.manage", "Gestionar facturas"),
        ("security.manage", "Gestionar roles/permisos"),
        ("airport.agent", "Operación aeroportuaria (agente)"),
        ("reports.view", "Consultar reportes")
    };

    foreach (var (name, desc) in permissionNames)
    {
        var perm = await context.Set<PermissionEntity>()
            .FirstOrDefaultAsync(p => p.Name != null && p.Name == name);

        if (perm is null)
        {
            perm = new PermissionEntity { Name = name, Description = desc };
            context.Set<PermissionEntity>().Add(perm);
            await context.SaveChangesAsync();
        }

        var linkExists = await context.Set<RolePermissionEntity>()
            .AsNoTracking()
            .AnyAsync(rp => rp.RoleId == role.Id && rp.PermissionId == perm.Id);

        if (!linkExists)
        {
            context.Set<RolePermissionEntity>().Add(new RolePermissionEntity
            {
                RoleId = role.Id,
                PermissionId = perm.Id
            });
            await context.SaveChangesAsync();
        }
    }

    var existing = await context.Set<UserEntity>().FirstOrDefaultAsync(u =>
        u.Username != null && u.Username.ToUpper() == username.ToUpperInvariant()
    );
    if (existing is not null)
    {
        existing.PasswordHash = passwordHash;
        existing.IsActive = true;
        existing.SystemRoleId = role.Id;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        Console.WriteLine("ROOT ya existía. Contraseña actualizada a 12345 y activado.");
        return;
    }

    var now = DateTime.UtcNow;
    var user = new UserEntity
    {
        Username = username,
        PasswordHash = passwordHash,
        PersonId = null,
        SystemRoleId = role.Id,
        IsActive = true,
        LastAccessAt = null,
        CreatedAt = now,
        UpdatedAt = now
    };

    context.Set<UserEntity>().Add(user);
    await context.SaveChangesAsync();

    Console.WriteLine($"Usuario ROOT creado (role_id={role.Id}). Contraseña: 12345");
}

static async Task DescribeTableAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context, string table)
{
    var cols = await context.Database.SqlQueryRaw<string>(
        "SELECT COLUMN_NAME AS Value FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = {0} ORDER BY ORDINAL_POSITION",
        table
    ).ToListAsync();

    Console.WriteLine($"Columnas de la tabla `{table}` ({cols.Count}):");
    foreach (var c in cols)
    {
        Console.WriteLine($"- {c}");
    }
}

static async Task ValidateMappingsAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context)
{
    // Compara el modelo EF contra el esquema real en MySQL (information_schema)
    // para detectar "Unknown column" antes de que el usuario lo vea en runtime.

    var entityTypes = context.Model.GetEntityTypes()
        .Where(et => !et.IsOwned())
        .OrderBy(et => et.GetTableName())
        .ThenBy(et => et.Name)
        .ToList();

    Console.WriteLine($"Entidades a validar: {entityTypes.Count}");

    foreach (var et in entityTypes)
    {
        var table = et.GetTableName();
        if (string.IsNullOrWhiteSpace(table))
        {
            continue;
        }

        var dbCols = await context.Database.SqlQueryRaw<string>(
            "SELECT COLUMN_NAME AS Value FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = {0}",
            table
        ).ToListAsync();

        if (dbCols.Count == 0)
        {
            Console.WriteLine($"[MISSING TABLE?] `{table}` (Entity: {et.Name}) - no se encontraron columnas en information_schema.");
            continue;
        }

        var dbSet = new HashSet<string>(dbCols, StringComparer.OrdinalIgnoreCase);
        var storeId = StoreObjectIdentifier.Table(table);

        var mappedCols = et.GetProperties()
            .Select(p => p.GetColumnName(storeId))
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        var missing = mappedCols.Where(c => !dbSet.Contains(c!)).ToList();
        if (missing.Count > 0)
        {
            Console.WriteLine($"[MISMATCH] `{table}` (Entity: {et.Name})");
            foreach (var m in missing)
            {
                Console.WriteLine($"  - columna mapeada no existe en DB: `{m}`");
            }
        }
    }

    Console.WriteLine("Validación terminada.");
}

static async Task NormalizePersonsColumnsAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context)
{
    // Normaliza nombres de columnas de `persons` (PascalCase -> snake_case) cuando la BD fue creada
    // desde un script legacy. Evita depender de nombres de foreign keys.
    var cols = await context.Database.SqlQueryRaw<string>(
        "SELECT COLUMN_NAME AS Value FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = 'persons'"
    ).ToListAsync();

    if (cols.Count == 0)
    {
        Console.WriteLine("Tabla `persons` no existe.");
        return;
    }

    var set = new HashSet<string>(cols, StringComparer.OrdinalIgnoreCase);

    async Task RenameIfExistsAsync(string from, string to)
    {
        if (!set.Contains(from) || set.Contains(to))
            return;
        // Column names are internal constants (not user input). Suppress EF1002 for this safe use.
#pragma warning disable EF1002
        await context.Database.ExecuteSqlRawAsync($"ALTER TABLE `persons` RENAME COLUMN `{from}` TO `{to}`;");
#pragma warning restore EF1002
        set.Remove(from);
        set.Add(to);
    }

    await RenameIfExistsAsync("Id", "id");
    await RenameIfExistsAsync("DocumentTypeId", "document_type_id");
    await RenameIfExistsAsync("DocumentNumber", "document_number");
    await RenameIfExistsAsync("FirstName", "first_name");
    await RenameIfExistsAsync("LastName", "last_name");
    await RenameIfExistsAsync("BirthDate", "birth_date");
    await RenameIfExistsAsync("Gender", "gender");
    await RenameIfExistsAsync("CreatedAt", "created_at");
    await RenameIfExistsAsync("UpdatedAt", "updated_at");
}

static async Task EnsureDefaultsAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context)
{
    // Seed global de catálogos (cada módulo aporta su propio seeder).
    await sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed.CatalogSeed.SeedAsync(context);

    // Seed específico que el módulo de Reservas requiere por compatibilidad (idempotente).
    await ReservationStatusSeeder.EnsureAsync(context);
}

static async Task DebugRootAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context)
{
    Console.WriteLine("== Debug ROOT (smoke checks) ==");

    // 0) Infra: mappings + connection
    await ValidateMappingsAsync(context);

    // 1) Login: verify ROOT exists & password
    var root = await context.Set<UserEntity>()
        .AsNoTracking()
        .FirstOrDefaultAsync(u => u.Username != null && u.Username.ToUpper() == "ROOT");
    Console.WriteLine(root is null ? "[FAIL] ROOT no existe" : $"[OK] ROOT existe (id={root.Id}, role_id={root.SystemRoleId})");

    // 2) Catálogos base (should exist / be queryable)
    async Task PrintCountAsync<T>(string label) where T : class
    {
        try
        {
            var count = await context.Set<T>().AsNoTracking().CountAsync();
            Console.WriteLine($"[OK] {label}: {count}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[FAIL] {label}: {ex.Message}");
        }
    }

    await PrintCountAsync<sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity.AirlineEntity>("Aerolíneas");
    await PrintCountAsync<sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity.AirportEntity>("Aeropuertos");
    await PrintCountAsync<sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity.RouteEntity>("Rutas");
    await PrintCountAsync<sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity.FlightEntity>("Vuelos");
    await PrintCountAsync<sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity.ReservationStatusEntity>("Estados de reserva");

    // 3) Business checks: flights have seats > 0 and can be listed
    try
    {
        var flights = await context.Set<sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity.FlightEntity>()
            .AsNoTracking()
            .OrderBy(f => f.DepartureDate)
            .Take(5)
            .Select(f => new { f.Id, f.FlightCode, f.AvailableSeats, f.DepartureDate })
            .ToListAsync();
        Console.WriteLine(flights.Count == 0
            ? "[WARN] No hay vuelos para probar reservas"
            : $"[OK] Vuelos consultables (top5): {string.Join(", ", flights.Select(f => $"{f.FlightCode}#{f.Id}({f.AvailableSeats})"))}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[FAIL] Consulta de vuelos: {ex.Message}");
    }

    // 4) Reservation flow sanity: ensure booking_statuses has Pendiente(1)
    try
    {
        await ReservationStatusSeeder.EnsureAsync(context);
        var pending = await context.Database.SqlQueryRaw<int>(
            "SELECT id AS Value FROM booking_statuses WHERE id = 1"
        ).ToListAsync();
        Console.WriteLine(pending.Count == 1 ? "[OK] booking_statuses contiene Pendiente (1)" : "[FAIL] booking_statuses sigue sin Pendiente(1)");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[FAIL] booking_statuses: {ex.Message}");
    }

    Console.WriteLine("== Fin debug ==");
}

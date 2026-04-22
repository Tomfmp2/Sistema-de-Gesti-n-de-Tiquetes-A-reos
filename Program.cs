using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Infrastructure.Entity;

try
{
    SpectreUi.InitializeConsoleUi();

    var context = DbContextFactory.Create();

    if (context.Database.CanConnect())
    {
        Console.WriteLine("Conexion exitosa");

        var describeArg = args.FirstOrDefault(a => a.StartsWith("--describe-table=", StringComparison.OrdinalIgnoreCase));
        if (describeArg is not null)
        {
            var table = describeArg.Split('=', 2)[1];
            await DescribeTableAsync(context, table);
            return;
        }

        if (args.Any(a => string.Equals(a, "--seed-root", StringComparison.OrdinalIgnoreCase)))
        {
            await SeedRootUserAsync(context);
            return;
        }

        // Opcional: aplicar migraciones solo si se solicita explícitamente
        if (string.Equals(Environment.GetEnvironmentVariable("APPLY_MIGRATIONS"), "true", StringComparison.OrdinalIgnoreCase))
        {
            context.Database.Migrate();
            Console.WriteLine("Migraciones aplicadas correctamente");
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
        Console.WriteLine("No se pudo conectar a la base de datos");
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

static async Task SeedRootUserAsync(sistema_gestor_de_tiquetes_aereos.Src.Shared.Context.AppDbContext context)
{
    const string username = "ROOT";
    const string passwordHash = "12345"; // LoginShell soporta password plano o SHA-256 hex

    var existing = await context.Set<UserEntity>().FirstOrDefaultAsync(u => u.Username == username);
    if (existing is not null)
    {
        existing.PasswordHash = passwordHash;
        existing.IsActive = true;
        existing.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();
        Console.WriteLine("ROOT ya existía. Contraseña actualizada a 12345 y activado.");
        return;
    }

    var role = await context.Set<SystemRoleEntity>()
        .OrderBy(r => r.Id)
        .FirstOrDefaultAsync(r => r.Name == "admin" || r.Name == "ADMIN" || r.Name == "root" || r.Name == "ROOT");

    if (role is null)
    {
        role = new SystemRoleEntity { Name = "admin", Description = "Administrador del sistema" };
        context.Set<SystemRoleEntity>().Add(role);
        await context.SaveChangesAsync();
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

    Console.WriteLine($"Table `{table}` columns ({cols.Count}):");
    foreach (var c in cols)
    {
        Console.WriteLine($"- {c}");
    }
}

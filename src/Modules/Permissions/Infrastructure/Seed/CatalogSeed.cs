using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.Permissions.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Name = "reservations.manage", Description = "Gestionar reservas" },
            new { Name = "flights.manage", Description = "Gestionar vuelos" },
            new { Name = "catalogs.manage", Description = "Gestionar catálogos del sistema" },
            new { Name = "payments.manage", Description = "Gestionar pagos" },
            new { Name = "reports.view", Description = "Consultar reportes" },
        };

        foreach (var p in desired)
        {
            var norm = SeedHelpers.Normalize(p.Name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.Permissions.Add(new PermissionEntity { Name = p.Name, Description = p.Description });
        }

        await context.SaveChangesAsync();
    }
}


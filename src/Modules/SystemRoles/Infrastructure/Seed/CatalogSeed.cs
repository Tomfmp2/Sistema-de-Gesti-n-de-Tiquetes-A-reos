using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.SystemRoles.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Name = "admin", Description = "Acceso completo al sistema" },
            new { Name = "Agente", Description = "Gestion de ventas, reservas y atencion al cliente" },
            new { Name = "Cliente", Description = "Acceso de autoservicio para pasajeros" },
            new { Name = "Operaciones", Description = "Gestion operativa de vuelos y tripulacion" },
        };

        foreach (var r in desired)
        {
            var norm = SeedHelpers.Normalize(r.Name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.SystemRoles.Add(new SystemRoleEntity { Name = r.Name, Description = r.Description });
        }

        await context.SaveChangesAsync();
    }
}


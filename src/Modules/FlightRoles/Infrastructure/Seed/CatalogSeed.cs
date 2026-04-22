using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.FlightRoles.AsNoTracking().ToListAsync();
        var desired = new[] { "Capitan", "Primer oficial", "Jefe de cabina", "Auxiliar de vuelo" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.FlightRoles.Add(new FlightRoleEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


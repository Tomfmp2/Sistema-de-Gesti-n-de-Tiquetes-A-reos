using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.StreetTypes.AsNoTracking().ToListAsync();
        var desired = new[] { "Calle", "Carrera", "Avenida", "Diagonal", "Transversal" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.StreetTypes.Add(new StreetTypeEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


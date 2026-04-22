using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.CabinTypes.AsNoTracking().ToListAsync();
        var desired = new[] { "Economica", "Premium Economy", "Ejecutiva", "Primera clase" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.CabinTypes.Add(new CabinTypeEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


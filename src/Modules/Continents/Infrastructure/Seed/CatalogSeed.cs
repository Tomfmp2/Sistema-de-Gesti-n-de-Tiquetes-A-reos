using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        await SeedContinentsAsync(context);
    }

    private static async Task SeedContinentsAsync(AppDbContext context)
    {
        var existing = await context.Continents.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Id = 1, Name = "America" },
            new { Id = 2, Name = "Europa" },
            new { Id = 3, Name = "Asia" },
            new { Id = 4, Name = "Africa" },
            new { Id = 5, Name = "Oceania" },
            new { Id = 6, Name = "Antartida" }
        };

        foreach (var continent in desired)
        {
            var nameNorm = SeedHelpers.Normalize(continent.Name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == nameNorm))
                continue;

            context.Continents.Add(new ContinentEntity
            {
                Id = continent.Id,
                Name = continent.Name
            });
        }

        await context.SaveChangesAsync();
    }
}

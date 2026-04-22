using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.Seasons.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Name = "Baja", Description = "Temporada de menor demanda", Factor = 0.9m },
            new { Name = "Media", Description = "Temporada regular", Factor = 1.0m },
            new { Name = "Alta", Description = "Temporada de alta demanda", Factor = 1.25m },
            new { Name = "Festiva", Description = "Temporada de festivos y vacaciones", Factor = 1.4m },
        };

        foreach (var s in desired)
        {
            var norm = SeedHelpers.Normalize(s.Name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.Seasons.Add(new SeasonEntity
            {
                Name = s.Name,
                Description = s.Description,
                PriceFactor = s.Factor
            });
        }

        await context.SaveChangesAsync();
    }
}


using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Seasons.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in SeasonDefaultData.Seasons)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Seasons.Add(new SeasonEntity
            {
                Id = row.Id,
                Name = row.Name,
                Description = row.Description,
                PriceFactor = row.PriceFactor
            });
        }

        await context.SaveChangesAsync();
    }
}


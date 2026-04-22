using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Regions.AsNoTracking().Select(r => r.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in RegionDefaultData.Regions)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Regions.Add(new RegionEntity
            {
                Id = row.Id,
                Name = row.Name,
                Type = row.Type,
                CountryId = row.CountryId
            });
        }

        await context.SaveChangesAsync();
    }
}


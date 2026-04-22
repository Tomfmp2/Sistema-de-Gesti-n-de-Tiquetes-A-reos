using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Cities.AsNoTracking().Select(c => c.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in CityDefaultData.Cities)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Cities.Add(new CityEntity { Id = row.Id, Name = row.Name, RegionId = row.RegionId });
        }

        await context.SaveChangesAsync();
    }
}


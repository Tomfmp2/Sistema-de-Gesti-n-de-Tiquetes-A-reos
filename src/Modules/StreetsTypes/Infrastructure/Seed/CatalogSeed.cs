using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.StreetTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in StreetTypeDefaultData.StreetTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.StreetTypes.Add(new StreetTypeEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


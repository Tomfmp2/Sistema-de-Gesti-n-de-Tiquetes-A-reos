using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Fares.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in FareDefaultData.Fares)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Fares.Add(new FareEntity
            {
                Id = row.Id,
                RouteId = row.RouteId,
                CabinTypeId = row.CabinTypeId,
                PassengerTypeId = row.PassengerTypeId,
                SeasonId = row.SeasonId,
                BasePrice = row.BasePrice,
                ValidFrom = row.ValidFrom,
                ValidTo = row.ValidTo
            });
        }

        await context.SaveChangesAsync();
    }
}

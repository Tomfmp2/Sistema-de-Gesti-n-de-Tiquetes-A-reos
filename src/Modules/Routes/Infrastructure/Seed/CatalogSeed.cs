using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Routes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in RouteDefaultData.Routes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Routes.Add(new RouteEntity
            {
                Id = row.Id,
                OriginAirportId = row.OriginAirportId,
                DestinationAirportId = row.DestinationAirportId,
                DistanceKm = row.DistanceKm,
                EstimatedDurationMin = row.EstimatedDurationMin
            });
        }

        await context.SaveChangesAsync();
    }
}

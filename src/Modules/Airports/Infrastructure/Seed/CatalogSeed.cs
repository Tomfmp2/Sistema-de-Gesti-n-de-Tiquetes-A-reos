using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Airports.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in AirportDefaultData.Airports)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Airports.Add(new AirportEntity
            {
                Id = row.Id,
                Name = row.Name,
                IataCode = row.IataCode,
                IcaoCode = row.IcaoCode,
                CityId = row.CityId
            });
        }

        await context.SaveChangesAsync();
    }
}

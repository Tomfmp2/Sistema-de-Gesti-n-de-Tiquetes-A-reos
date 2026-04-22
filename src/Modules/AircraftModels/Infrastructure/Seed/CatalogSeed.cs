using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.AircraftModels.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in AircraftModelDefaultData.AircraftModels)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.AircraftModels.Add(new AircraftModelEntity
            {
                Id = row.Id,
                ManufacturerId = row.ManufacturerId,
                ModelName = row.ModelName,
                MaxCapacity = row.MaxCapacity,
                MaxTakeoffWeightKg = row.MaxTakeoffWeightKg,
                FuelConsumptionKgH = row.FuelConsumptionKgH,
                CruisingSpeedKmh = row.CruisingSpeedKmh,
                CruisingAltitudeFt = row.CruisingAltitudeFt
            });
        }

        await context.SaveChangesAsync();
    }
}

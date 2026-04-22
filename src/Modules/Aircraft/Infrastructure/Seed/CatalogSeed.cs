using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Aircraft.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in AircraftDefaultData.Aircraft)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Aircraft.Add(new AircraftEntity
            {
                Id = row.Id,
                ModelId = row.ModelId,
                AirlineId = row.AirlineId,
                Registration = row.Registration,
                ManufacturingDate = row.ManufacturingDate,
                IsActive = row.IsActive
            });
        }

        await context.SaveChangesAsync();
    }
}

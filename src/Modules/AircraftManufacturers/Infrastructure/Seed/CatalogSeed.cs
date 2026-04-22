using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.AircraftManufacturers.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in AircraftManufacturerDefaultData.AircraftManufacturers)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.AircraftManufacturers.Add(new AircraftManufacturerEntity
            {
                Id = row.Id,
                Name = row.Name,
                Country = row.Country
            });
        }

        await context.SaveChangesAsync();
    }
}


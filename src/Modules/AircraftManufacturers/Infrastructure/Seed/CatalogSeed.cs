using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.AircraftManufacturers.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Name = "Airbus", Country = "Francia" },
            new { Name = "Boeing", Country = "Estados Unidos" },
            new { Name = "Embraer", Country = "Brasil" },
            new { Name = "ATR", Country = "Francia" },
            new { Name = "De Havilland Canada", Country = "Canada" },
        };

        foreach (var m in desired)
        {
            var norm = SeedHelpers.Normalize(m.Name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.AircraftManufacturers.Add(new AircraftManufacturerEntity
            {
                Name = m.Name,
                Country = m.Country
            });
        }

        await context.SaveChangesAsync();
    }
}


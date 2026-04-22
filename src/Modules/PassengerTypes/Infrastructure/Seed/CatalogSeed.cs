using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.PassengerTypes.AsNoTracking().ToListAsync();
        var desired = new[]
        {
            new { Name = "Infante", MinAge = (int?)0, MaxAge = (int?)1 },
            new { Name = "Nino", MinAge = (int?)2, MaxAge = (int?)11 },
            new { Name = "Adulto", MinAge = (int?)12, MaxAge = (int?)null },
            new { Name = "Adulto mayor", MinAge = (int?)60, MaxAge = (int?)null },
        };

        foreach (var pt in desired)
        {
            var norm = SeedHelpers.Normalize(pt.Name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.PassengerTypes.Add(new PassengerTypeEntity
            {
                Name = pt.Name,
                MinAge = pt.MinAge,
                MaxAge = pt.MaxAge
            });
        }

        await context.SaveChangesAsync();
    }
}


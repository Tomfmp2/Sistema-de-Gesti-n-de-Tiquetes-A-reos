using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.FlightStatuses.AsNoTracking().ToListAsync();
        var desired = new[] { "Programado", "Abordando", "En vuelo", "Aterrizado", "Retrasado", "Cancelado" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.FlightStatuses.Add(new FlightStatusEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


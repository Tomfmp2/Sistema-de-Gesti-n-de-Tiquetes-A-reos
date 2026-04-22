using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.PassengerTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in PassengerTypeDefaultData.PassengerTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.PassengerTypes.Add(new PassengerTypeEntity
            {
                Id = row.Id,
                Name = row.Name,
                MinAge = row.MinAge,
                MaxAge = row.MaxAge
            });
        }

        await context.SaveChangesAsync();
    }
}


using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.BaggageTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in BaggageTypeDefaultData.BaggageTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.BaggageTypes.Add(new BaggageTypeEntity
            {
                Id = row.Id,
                Name = row.Name,
                MaxWeightKg = row.MaxWeightKg,
                BasePrice = row.BasePrice
            });
        }

        await context.SaveChangesAsync();
    }
}

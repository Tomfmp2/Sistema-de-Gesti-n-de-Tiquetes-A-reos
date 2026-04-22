using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.CardTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in CardTypeDefaultData.CardTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.CardTypes.Add(new CardTypeEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


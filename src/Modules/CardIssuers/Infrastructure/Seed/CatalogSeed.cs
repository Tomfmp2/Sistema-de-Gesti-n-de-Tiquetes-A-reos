using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.CardIssuers.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in CardIssuerDefaultData.CardIssuers)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.CardIssuers.Add(new CardIssuerEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


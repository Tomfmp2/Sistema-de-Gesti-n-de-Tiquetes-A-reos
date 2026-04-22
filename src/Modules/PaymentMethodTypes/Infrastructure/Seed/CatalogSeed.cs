using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.PaymentMethodTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in PaymentMethodTypeDefaultData.PaymentMethodTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.PaymentMethodTypes.Add(new PaymentMethodTypeEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.PaymentMethods.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in PaymentMethodDefaultData.PaymentMethods)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.PaymentMethods.Add(new PaymentMethodEntity
            {
                Id = row.Id,
                PaymentMethodTypeId = row.PaymentMethodTypeId,
                CardTypeId = row.CardTypeId,
                CardIssuerId = row.CardIssuerId,
                CommercialName = row.CommercialName
            });
        }

        await context.SaveChangesAsync();
    }
}


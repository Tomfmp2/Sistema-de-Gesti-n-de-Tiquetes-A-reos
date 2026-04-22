using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.PaymentStatuses.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in PaymentStatusDefaultData.PaymentStatuses)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.PaymentStatuses.Add(new PaymentStatusEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.InvoiceItemTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in InvoiceItemTypeDefaultData.InvoiceItemTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.InvoiceItemTypes.Add(new InvoiceItemTypeEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


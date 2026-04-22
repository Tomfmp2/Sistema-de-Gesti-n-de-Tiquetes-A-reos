using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.InvoiceItemTypes.AsNoTracking().ToListAsync();
        var desired = new[] { "Tarifa aerea", "Impuestos", "Equipaje", "Servicio", "Descuento" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.InvoiceItemTypes.Add(new InvoiceItemTypeEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


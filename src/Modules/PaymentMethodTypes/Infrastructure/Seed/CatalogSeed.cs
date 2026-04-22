using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.PaymentMethodTypes.AsNoTracking().ToListAsync();
        var desired = new[] { "Tarjeta", "Efectivo", "Transferencia bancaria", "Billetera digital" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.PaymentMethodTypes.Add(new PaymentMethodTypeEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


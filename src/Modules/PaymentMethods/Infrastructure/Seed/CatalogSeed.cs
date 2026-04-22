using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var types = await context.PaymentMethodTypes.AsNoTracking().ToListAsync();
        var cardTypes = await context.CardTypes.AsNoTracking().ToListAsync();
        var issuers = await context.CardIssuers.AsNoTracking().ToListAsync();

        int TypeId(string name)
        {
            var norm = SeedHelpers.Normalize(name);
            var x = types.FirstOrDefault(t => SeedHelpers.Normalize(t.Name) == norm);
            if (x is null)
                throw new InvalidOperationException($"No existe PaymentMethodType '{name}'. Ejecuta seed de PaymentMethodTypes primero.");
            return x.Id;
        }

        int CardTypeId(string name)
        {
            var norm = SeedHelpers.Normalize(name);
            var x = cardTypes.FirstOrDefault(t => SeedHelpers.Normalize(t.Name) == norm);
            if (x is null)
                throw new InvalidOperationException($"No existe CardType '{name}'. Ejecuta seed de CardTypes primero.");
            return x.Id;
        }

        int IssuerId(string name)
        {
            var norm = SeedHelpers.Normalize(name);
            var x = issuers.FirstOrDefault(t => SeedHelpers.Normalize(t.Name) == norm);
            if (x is null)
                throw new InvalidOperationException($"No existe CardIssuer '{name}'. Ejecuta seed de CardIssuers primero.");
            return x.Id;
        }

        var existing = await context.PaymentMethods.AsNoTracking().ToListAsync();

        var desired = new[]
        {
            new PaymentMethodEntity { PaymentMethodTypeId = TypeId("Tarjeta"), CardTypeId = CardTypeId("Credito"), CardIssuerId = IssuerId("Visa"), CommercialName = "Visa credito" },
            new PaymentMethodEntity { PaymentMethodTypeId = TypeId("Tarjeta"), CardTypeId = CardTypeId("Credito"), CardIssuerId = IssuerId("Mastercard"), CommercialName = "Mastercard credito" },
            new PaymentMethodEntity { PaymentMethodTypeId = TypeId("Tarjeta"), CardTypeId = CardTypeId("Debito"), CardIssuerId = IssuerId("Visa"), CommercialName = "Visa debito" },
            new PaymentMethodEntity { PaymentMethodTypeId = TypeId("Tarjeta"), CardTypeId = CardTypeId("Credito"), CardIssuerId = IssuerId("American Express"), CommercialName = "American Express credito" },
            new PaymentMethodEntity { PaymentMethodTypeId = TypeId("Efectivo"), CardTypeId = null, CardIssuerId = null, CommercialName = "Efectivo en oficina" },
            new PaymentMethodEntity { PaymentMethodTypeId = TypeId("Transferencia bancaria"), CardTypeId = null, CardIssuerId = null, CommercialName = "Transferencia bancaria" },
        };

        foreach (var pm in desired)
        {
            var norm = SeedHelpers.Normalize(pm.CommercialName);
            if (existing.Any(x => SeedHelpers.Normalize(x.CommercialName) == norm))
                continue;

            context.PaymentMethods.Add(pm);
        }

        await context.SaveChangesAsync();
    }
}


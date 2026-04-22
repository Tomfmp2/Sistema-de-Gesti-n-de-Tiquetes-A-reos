using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.CardIssuers.AsNoTracking().ToListAsync();
        var desired = new[] { "Visa", "Mastercard", "American Express", "Diners Club" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.CardIssuers.Add(new CardIssuerEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


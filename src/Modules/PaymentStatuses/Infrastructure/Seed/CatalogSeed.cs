using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.PaymentStatuses.AsNoTracking().ToListAsync();
        var desired = new[] { "Pendiente", "Aprobado", "Rechazado", "Reembolsado", "Anulado" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.PaymentStatuses.Add(new PaymentStatusEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


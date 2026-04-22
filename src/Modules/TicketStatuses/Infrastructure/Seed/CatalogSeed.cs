using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.TicketStatuses.AsNoTracking().ToListAsync();
        var desired = new[] { "Emitido", "Usado", "Cancelado", "Reembolsado", "No presentado" };

        foreach (var name in desired)
        {
            var norm = SeedHelpers.Normalize(name);
            if (existing.Any(x => SeedHelpers.Normalize(x.Name) == norm))
                continue;

            context.TicketStatuses.Add(new TicketStatusEntity { Name = name });
        }

        await context.SaveChangesAsync();
    }
}


using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.ReservationStatuses.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in ReservationStatusDefaultData.ReservationStatuses)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.ReservationStatuses.Add(new ReservationStatusEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


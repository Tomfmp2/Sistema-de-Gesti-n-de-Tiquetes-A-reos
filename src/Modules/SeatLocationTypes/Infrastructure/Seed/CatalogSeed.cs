using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.SeatLocationTypes.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in SeatLocationTypeDefaultData.SeatLocationTypes)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.SeatLocationTypes.Add(new SeatLocationTypeEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


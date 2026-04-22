using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.StaffPositions.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in StaffPositionDefaultData.StaffPositions)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.StaffPositions.Add(new StaffPositionEntity { Id = row.Id, Name = row.Name });
        }

        await context.SaveChangesAsync();
    }
}


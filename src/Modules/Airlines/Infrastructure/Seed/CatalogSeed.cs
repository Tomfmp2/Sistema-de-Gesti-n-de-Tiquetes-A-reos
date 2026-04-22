using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Airlines.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in AirlineDefaultData.Airlines)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Airlines.Add(new AirlineEntity
            {
                Id = row.Id,
                Name = row.Name,
                IataCode = row.IataCode,
                OriginCountryId = row.OriginCountryId,
                IsActive = row.IsActive,
                CreatedAt = row.CreatedAt,
                UpdatedAt = row.UpdatedAt
            });
        }

        await context.SaveChangesAsync();
    }
}

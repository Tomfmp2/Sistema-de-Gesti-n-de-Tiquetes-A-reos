using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existing = await context.CabinConfiguration.AsNoTracking().Select(x => x.Id).ToListAsync();
        var have = existing.ToHashSet();
        foreach (var row in CabinConfigurationDefaultData.CabinConfigurations)
        {
            if (have.Contains(row.Id))
                continue;
            context.CabinConfiguration.Add(
                new CabinConfigurationEntity
                {
                    Id = row.Id,
                    AircraftId = row.AircraftId,
                    CabinTypeId = row.CabinTypeId,
                    StartRow = row.StartRow,
                    EndRow = row.EndRow,
                    SeatsPerRow = row.SeatsPerRow,
                    SeatLetters = row.SeatLetters
                }
            );
        }

        await context.SaveChangesAsync();
    }
}

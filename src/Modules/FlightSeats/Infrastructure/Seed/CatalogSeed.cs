using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.FlightSeats.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in FlightSeatDefaultData.FlightSeats)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.FlightSeats.Add(new FlightSeatEntity
            {
                Id = row.Id,
                FlightId = row.FlightId,
                SeatCode = row.SeatCode,
                CabinTypeId = row.CabinTypeId,
                LocationTypeId = row.LocationTypeId,
                Status = row.Status
            });
        }

        await context.SaveChangesAsync();
    }
}

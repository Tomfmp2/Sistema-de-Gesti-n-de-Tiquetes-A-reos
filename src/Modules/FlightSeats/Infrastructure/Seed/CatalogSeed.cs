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
                IsOccupied = row.IsOccupied
            });
        }

        await context.SaveChangesAsync();
        await SyncFlightCapacitiesFromSeatsAsync(context);
    }

    private static async Task SyncFlightCapacitiesFromSeatsAsync(AppDbContext context)
    {
        var flightIds = await context.Flights.AsNoTracking().Select(f => f.Id).ToListAsync();
        foreach (var fid in flightIds)
        {
            var total = await context.FlightSeats.CountAsync(s => s.FlightId == fid);
            var free = await context.FlightSeats.CountAsync(s => s.FlightId == fid && !s.IsOccupied);
            var f = await context.Flights.FirstAsync(x => x.Id == fid);
            f.TotalCapacity = total;
            f.AvailableSeats = free;
        }

        await context.SaveChangesAsync();
    }
}

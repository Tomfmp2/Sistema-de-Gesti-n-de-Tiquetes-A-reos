using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Seed;

public static class CatalogSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        var existingIds = await context.Flights.AsNoTracking().Select(x => x.Id).ToListAsync();
        var idSet = existingIds.ToHashSet();

        foreach (var row in FlightDefaultData.Flights)
        {
            if (idSet.Contains(row.Id))
                continue;

            context.Flights.Add(new FlightEntity
            {
                Id = row.Id,
                FlightCode = row.FlightCode,
                AirlineId = row.AirlineId,
                RouteId = row.RouteId,
                AircraftId = row.AircraftId,
                DepartureDate = row.DepartureDate,
                EstimatedArrivalDate = row.EstimatedArrivalDate,
                TotalCapacity = row.TotalCapacity,
                AvailableSeats = row.AvailableSeats,
                FlightStatusId = row.FlightStatusId,
                RescheduledAt = row.RescheduledAt,
                CreatedAt = row.CreatedAt,
                UpdatedAt = row.UpdatedAt
            });
        }

        await context.SaveChangesAsync();
    }
}

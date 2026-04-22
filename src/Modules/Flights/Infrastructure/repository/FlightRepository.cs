using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.repository;

public sealed class FlightRepository : IFlightRepository
{
    private readonly AppDbContext _context;

    public FlightRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Flight?> GetByIdAsync(FlightId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<FlightEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Flight>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<FlightEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Flight> AddAsync(Flight entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new FlightEntity
        {
            FlightCode = entity.FlightCode.Value,
    AirlineId = entity.AirlineId.Value,
    RouteId = entity.RouteId.Value,
    AircraftId = entity.AircraftId.Value,
    DepartureDate = entity.DepartureDate.Value,
    EstimatedArrivalDate = entity.EstimatedArrivalDate.Value,
    TotalCapacity = entity.TotalCapacity.Value,
    AvailableSeats = entity.AvailableSeats.Value,
    FlightStatusId = entity.FlightStatusId.Value,
    RescheduledAt = entity.RescheduledAt.Value,
    CreatedAt = entity.CreatedAt.Value,
    UpdatedAt = entity.UpdatedAt.Value,
        };
        _context.Set<FlightEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Flight entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<FlightEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe flight {entity.Id.Value}.");
        }

        e.FlightCode = entity.FlightCode.Value;
e.AirlineId = entity.AirlineId.Value;
e.RouteId = entity.RouteId.Value;
e.AircraftId = entity.AircraftId.Value;
e.DepartureDate = entity.DepartureDate.Value;
e.EstimatedArrivalDate = entity.EstimatedArrivalDate.Value;
e.TotalCapacity = entity.TotalCapacity.Value;
e.AvailableSeats = entity.AvailableSeats.Value;
e.FlightStatusId = entity.FlightStatusId.Value;
e.RescheduledAt = entity.RescheduledAt.Value;
e.CreatedAt = entity.CreatedAt.Value;
e.UpdatedAt = entity.UpdatedAt.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FlightId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<FlightEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<FlightEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Flight ToDomain(FlightEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.FlightCode);
        return Flight.Create(
            FlightId.Create(e.Id),
    FlightCode.Create(e.FlightCode),
    FlightAirlineId.Create(e.AirlineId),
    FlightRouteId.Create(e.RouteId),
    FlightAircraftId.Create(e.AircraftId),
    FlightDepartureDate.Create(e.DepartureDate),
    FlightEstimatedArrivalDate.Create(e.EstimatedArrivalDate),
    FlightTotalCapacity.Create(e.TotalCapacity),
    FlightAvailableSeats.Create(e.AvailableSeats),
    FlightStatusId.Create(e.FlightStatusId),
    FlightRescheduledAt.Create(e.RescheduledAt),
    FlightCreatedAt.Create(e.CreatedAt),
    FlightUpdatedAt.Create(e.UpdatedAt)
        );
    }
}

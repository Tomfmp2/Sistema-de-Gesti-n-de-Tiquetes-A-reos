using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Infrastructure.repository;

public sealed class FlightSeatRepository : IFlightSeatRepository
{
    private readonly DbContext _context;
    private readonly DbSet<FlightSeatEntity> _dbSet;

    public FlightSeatRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<FlightSeatEntity>();
    }

    public async Task<FlightSeat?> GetByIdAsync(FlightSeatId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        
        if (entity == null)
            return null;

        return MapToDomain(entity);
    }

    public async Task<IEnumerable<FlightSeat>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.ToListAsync(cancellationToken);
        return entities.Select(MapToDomain).ToList();
    }

    public async Task<FlightSeat> AddAsync(FlightSeat flightSeat, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(flightSeat);
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return FlightSeat.Reconstitute(
            new FlightSeatId(entity.Id),
            flightSeat.FlightId,
            flightSeat.SeatCode,
            flightSeat.CabinTypeId,
            flightSeat.LocationTypeId,
            flightSeat.IsOccupied);
    }

    public async Task<FlightSeat> UpdateAsync(FlightSeat flightSeat, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(flightSeat);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return flightSeat;
    }

    public async Task<bool> DeleteAsync(FlightSeatId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static FlightSeat MapToDomain(FlightSeatEntity entity)
    {
        return FlightSeat.Reconstitute(
            new FlightSeatId(entity.Id),
            new FlightId(entity.FlightId),
            new SeatCode(entity.SeatCode),
            new CabinTypeId(entity.CabinTypeId),
            new LocationTypeId(entity.LocationTypeId),
            entity.IsOccupied);
    }

    private static FlightSeatEntity MapToEntity(FlightSeat flightSeat)
    {
        return new FlightSeatEntity
        {
            Id = flightSeat.Id.Value,
            FlightId = flightSeat.FlightId.Value,
            SeatCode = flightSeat.SeatCode.Value,
            CabinTypeId = flightSeat.CabinTypeId.Value,
            LocationTypeId = flightSeat.LocationTypeId.Value,
            IsOccupied = flightSeat.IsOccupied
        };
    }
}

using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.repository;

public sealed class ReservationPassengerRepository : IReservationPassengerRepository
{
    private readonly AppDbContext _context;

    public ReservationPassengerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReservationPassenger?> GetByIdAsync(ReservationPassengerId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<ReservationPassengerEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<ReservationPassenger>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<ReservationPassengerEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<ReservationPassenger> AddAsync(ReservationPassenger entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new ReservationPassengerEntity
        {
            ReservationFlightId = entity.ReservationFlightId.Value,
    PassengerId = entity.PassengerId.Value,
        };
        _context.Set<ReservationPassengerEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(ReservationPassenger entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<ReservationPassengerEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe reservationpassenger {entity.Id.Value}.");
        }

        e.ReservationFlightId = entity.ReservationFlightId.Value;
e.PassengerId = entity.PassengerId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReservationPassengerId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<ReservationPassengerEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<ReservationPassengerEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static ReservationPassenger ToDomain(ReservationPassengerEntity e)
    {
        return ReservationPassenger.Create(
            ReservationPassengerId.Create(e.Id),
    ReservationPassengerReservationFlightId.Create(e.ReservationFlightId),
    ReservationPassengerPassengerId.Create(e.PassengerId)
        );
    }
}

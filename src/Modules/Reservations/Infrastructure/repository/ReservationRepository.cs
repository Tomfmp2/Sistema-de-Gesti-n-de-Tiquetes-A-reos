using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.repository;

public sealed class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Reservation?> GetByIdAsync(ReservationId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<ReservationEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Reservation>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<ReservationEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Reservation> AddAsync(Reservation entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new ReservationEntity
        {
            ClientId = entity.ClientId.Value,
    ReservationDate = entity.ReservationDate.Value,
    ReservationStatusId = entity.ReservationStatusId.Value,
    TotalValue = entity.TotalValue.Value,
    ExpiresAt = entity.ExpiresAt.Value,
    CreatedAt = entity.CreatedAt.Value,
    UpdatedAt = entity.UpdatedAt.Value,
        };
        _context.Set<ReservationEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Reservation entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<ReservationEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe reservation {entity.Id.Value}.");
        }

        e.ClientId = entity.ClientId.Value;
e.ReservationDate = entity.ReservationDate.Value;
e.ReservationStatusId = entity.ReservationStatusId.Value;
e.TotalValue = entity.TotalValue.Value;
e.ExpiresAt = entity.ExpiresAt.Value;
e.CreatedAt = entity.CreatedAt.Value;
e.UpdatedAt = entity.UpdatedAt.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(ReservationId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<ReservationEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<ReservationEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Reservation ToDomain(ReservationEntity e)
    {
        return Reservation.Create(
            ReservationId.Create(e.Id),
    ReservationClientId.Create(e.ClientId),
    ReservationDate.Create(e.ReservationDate),
    ReservationStatusId.Create(e.ReservationStatusId),
    ReservationTotalValue.Create(e.TotalValue),
    ReservationExpiresAt.Create(e.ExpiresAt),
    ReservationCreatedAt.Create(e.CreatedAt),
    ReservationUpdatedAt.Create(e.UpdatedAt)
        );
    }
}

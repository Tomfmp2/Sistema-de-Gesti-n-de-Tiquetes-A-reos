using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Infrastructure.repository;

public sealed class CheckinRepository : ICheckinRepository
{
    private readonly AppDbContext _context;

    public CheckinRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Checkin?> GetByIdAsync(CheckinId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<CheckinEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Checkin>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<CheckinEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Checkin> AddAsync(Checkin entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new CheckinEntity
        {
            TicketId = entity.TicketId.Value,
    StaffId = entity.StaffId.Value,
    FlightSeatId = entity.FlightSeatId.Value,
    CheckinDate = entity.CheckinDate.Value,
    CheckinStatusId = entity.CheckinStatusId.Value,
    BoardingPassNumber = entity.BoardingPassNumber.Value,
    HasCheckedBaggage = entity.HasCheckedBaggage.Value,
    BaggageWeightKg = entity.BaggageWeightKg.Value,
        };
        _context.Set<CheckinEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Checkin entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<CheckinEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe checkin {entity.Id.Value}.");
        }

        e.TicketId = entity.TicketId.Value;
e.StaffId = entity.StaffId.Value;
e.FlightSeatId = entity.FlightSeatId.Value;
e.CheckinDate = entity.CheckinDate.Value;
e.CheckinStatusId = entity.CheckinStatusId.Value;
e.BoardingPassNumber = entity.BoardingPassNumber.Value;
e.HasCheckedBaggage = entity.HasCheckedBaggage.Value;
e.BaggageWeightKg = entity.BaggageWeightKg.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CheckinId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<CheckinEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<CheckinEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Checkin ToDomain(CheckinEntity e)
    {
        return Checkin.Create(
            CheckinId.Create(e.Id),
    CheckinTicketId.Create(e.TicketId),
    CheckinStaffId.Create(e.StaffId),
    CheckinFlightSeatId.Create(e.FlightSeatId),
    CheckinDate.Create(e.CheckinDate),
    CheckinStatusId.Create(e.CheckinStatusId),
    CheckinBoardingPassNumber.Create(e.BoardingPassNumber),
    CheckinHasCheckedBaggage.Create(e.HasCheckedBaggage),
    CheckinBaggageWeightKg.Create(e.BaggageWeightKg)
        );
    }
}

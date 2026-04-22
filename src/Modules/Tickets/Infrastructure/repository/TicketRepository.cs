using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.repository;

public sealed class TicketRepository : ITicketRepository
{
    private readonly AppDbContext _context;

    public TicketRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetByIdAsync(TicketId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<TicketEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<TicketEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Ticket> AddAsync(Ticket entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new TicketEntity
        {
            ReservationPassengerId = entity.ReservationPassengerId.Value,
    Code = entity.Code.Value,
    IssueDate = entity.IssueDate.Value,
    TicketStatusId = entity.TicketStatusId.Value,
    CreatedAt = entity.CreatedAt.Value,
    UpdatedAt = entity.UpdatedAt.Value,
        };
        _context.Set<TicketEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Ticket entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<TicketEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe ticket {entity.Id.Value}.");
        }

        e.ReservationPassengerId = entity.ReservationPassengerId.Value;
e.Code = entity.Code.Value;
e.IssueDate = entity.IssueDate.Value;
e.TicketStatusId = entity.TicketStatusId.Value;
e.CreatedAt = entity.CreatedAt.Value;
e.UpdatedAt = entity.UpdatedAt.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TicketId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<TicketEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<TicketEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Ticket ToDomain(TicketEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Code);
        return Ticket.Create(
            TicketId.Create(e.Id),
    TicketReservationPassengerId.Create(e.ReservationPassengerId),
    TicketCode.Create(e.Code),
    TicketIssueDate.Create(e.IssueDate),
    TicketStatusId.Create(e.TicketStatusId),
    TicketCreatedAt.Create(e.CreatedAt),
    TicketUpdatedAt.Create(e.UpdatedAt)
        );
    }
}

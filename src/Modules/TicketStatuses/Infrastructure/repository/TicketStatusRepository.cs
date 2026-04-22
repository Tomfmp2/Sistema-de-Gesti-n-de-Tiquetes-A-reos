using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Infrastructure.repository;

public sealed class TicketStatusRepository : ITicketStatusRepository
{
    private readonly AppDbContext _context;

    public TicketStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TicketStatus?> GetByIdAsync(TicketStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<TicketStatusEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<TicketStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<TicketStatusEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<TicketStatus> AddAsync(TicketStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new TicketStatusEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<TicketStatusEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(TicketStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<TicketStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe ticketstatus {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(TicketStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<TicketStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<TicketStatusEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static TicketStatus ToDomain(TicketStatusEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Name);
        return TicketStatus.Create(
            TicketStatusId.Create(e.Id),
    TicketStatusName.Create(e.Name)
        );
    }
}

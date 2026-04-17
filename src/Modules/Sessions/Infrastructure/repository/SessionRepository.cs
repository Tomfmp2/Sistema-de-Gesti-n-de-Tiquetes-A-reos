using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Infrastructure.repository;

public sealed class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByIdAsync(SessionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<SessionEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Session>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<SessionEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Session> AddAsync(Session entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Session.CreateNew para insertar.");
        }

        var e = new SessionEntity
        {
            UserId = entity.UserId.Value,
            StartedAt = entity.StartedAt,
            ClosedAt = entity.ClosedAt,
            OriginIp = entity.OriginIp,
            IsActive = entity.IsActive,
        };
        _context.Set<SessionEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Session entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<SessionEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe sesión {entity.Id.Value}.");
        }

        e.UserId = entity.UserId.Value;
        e.StartedAt = entity.StartedAt;
        e.ClosedAt = entity.ClosedAt;
        e.OriginIp = entity.OriginIp;
        e.IsActive = entity.IsActive;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(SessionId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<SessionEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<SessionEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Session ToDomain(SessionEntity e)
    {
        return Session.Create(
            SessionId.Create(e.Id),
            SessionUserId.Create(e.UserId),
            e.StartedAt,
            e.ClosedAt,
            e.OriginIp,
            e.IsActive
        );
    }
}

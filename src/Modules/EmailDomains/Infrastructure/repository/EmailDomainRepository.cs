using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.repository;

public sealed class EmailDomainRepository : IEmailDomainRepository
{
    private readonly AppDbContext _context;

    public EmailDomainRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<EmailDomain?> GetByIdAsync(
        EmailDomainId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<EmailDomainEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<EmailDomain>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<EmailDomainEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<EmailDomain> AddAsync(
        EmailDomain entity,
        CancellationToken cancellationToken = default
    )
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use EmailDomain.CreateNew para insertar.");
        }

        var e = new EmailDomainEntity { Domain = entity.Domain.Value };
        _context.Set<EmailDomainEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(EmailDomain entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<EmailDomainEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe dominio de correo {entity.Id.Value}.");
        }

        e.Domain = entity.Domain.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(EmailDomainId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<EmailDomainEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<EmailDomainEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static EmailDomain ToDomain(EmailDomainEntity e)
    {
        return EmailDomain.Create(
            EmailDomainId.Create(e.Id),
            EmailDomainHost.Create(e.Domain ?? string.Empty)
        );
    }
}

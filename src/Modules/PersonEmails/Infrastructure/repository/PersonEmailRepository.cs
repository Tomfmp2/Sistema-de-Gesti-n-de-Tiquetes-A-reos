using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Infrastructure.repository;

public sealed class PersonEmailRepository : IPersonEmailRepository
{
    private readonly AppDbContext _context;

    public PersonEmailRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PersonEmail?> GetByIdAsync(
        PersonEmailId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PersonEmailEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PersonEmail>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<PersonEmailEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PersonEmail> AddAsync(
        PersonEmail entity,
        CancellationToken cancellationToken = default
    )
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use PersonEmail.CreateNew para insertar.");
        }

        var e = new PersonEmailEntity
        {
            PersonId = entity.PersonId.Value,
            EmailLocalPart = entity.EmailLocalPart.Value,
            EmailDomainId = entity.EmailDomainId.Value,
            IsPrimary = entity.IsPrimary,
        };
        _context.Set<PersonEmailEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PersonEmail entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PersonEmailEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe correo de persona {entity.Id.Value}.");
        }

        e.PersonId = entity.PersonId.Value;
        e.EmailLocalPart = entity.EmailLocalPart.Value;
        e.EmailDomainId = entity.EmailDomainId.Value;
        e.IsPrimary = entity.IsPrimary;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PersonEmailId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PersonEmailEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PersonEmailEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PersonEmail ToDomain(PersonEmailEntity e)
    {
        return PersonEmail.Create(
            PersonEmailId.Create(e.Id),
            PersonEmailPersonId.Create(e.PersonId),
            PersonEmailLocalPart.Create(e.EmailLocalPart ?? string.Empty),
            PersonEmailDomainRefId.Create(e.EmailDomainId),
            e.IsPrimary
        );
    }
}

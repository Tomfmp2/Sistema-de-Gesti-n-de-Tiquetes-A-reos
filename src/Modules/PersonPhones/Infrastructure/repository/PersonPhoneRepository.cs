using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Infrastructure.repository;

public sealed class PersonPhoneRepository : IPersonPhoneRepository
{
    private readonly AppDbContext _context;

    public PersonPhoneRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PersonPhone?> GetByIdAsync(
        PersonPhoneId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PersonPhoneEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PersonPhone>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<PersonPhoneEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PersonPhone> AddAsync(
        PersonPhone entity,
        CancellationToken cancellationToken = default
    )
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use PersonPhone.CreateNew para insertar.");
        }

        var e = new PersonPhoneEntity
        {
            PersonId = entity.PersonId.Value,
            PhoneCodeId = entity.PhoneCodeId.Value,
            Number = entity.Number.Value,
            IsPrimary = entity.IsPrimary,
        };
        _context.Set<PersonPhoneEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PersonPhone entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PersonPhoneEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe teléfono de persona {entity.Id.Value}.");
        }

        e.PersonId = entity.PersonId.Value;
        e.PhoneCodeId = entity.PhoneCodeId.Value;
        e.Number = entity.Number.Value;
        e.IsPrimary = entity.IsPrimary;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PersonPhoneId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PersonPhoneEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PersonPhoneEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PersonPhone ToDomain(PersonPhoneEntity e)
    {
        return PersonPhone.Create(
            PersonPhoneId.Create(e.Id),
            PersonPhonePersonId.Create(e.PersonId),
            PersonPhoneCodeRefId.Create(e.PhoneCodeId),
            PersonPhoneLineNumber.Create(e.Number ?? string.Empty),
            e.IsPrimary
        );
    }
}

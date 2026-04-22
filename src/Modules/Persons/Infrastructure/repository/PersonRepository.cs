using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.repository;

public sealed class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(PersonId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PersonEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<PersonEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Person> AddAsync(Person entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Person.CreateNew para insertar.");
        }

        var now = DateTime.UtcNow;
        var e = new PersonEntity
        {
            DocumentTypeId = entity.DocumentTypeId.Value,
            DocumentNumber = entity.DocumentNumber.Value,
            FirstName = entity.FirstName.Value,
            LastName = entity.LastName.Value,
            BirthDate = entity.BirthDate,
            Gender = entity.Gender,
            AddressId = entity.AddressId,
            CreatedAt = now,
            UpdatedAt = now,
        };
        _context.Set<PersonEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Person entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PersonEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe persona {entity.Id.Value}.");
        }

        e.DocumentTypeId = entity.DocumentTypeId.Value;
        e.DocumentNumber = entity.DocumentNumber.Value;
        e.FirstName = entity.FirstName.Value;
        e.LastName = entity.LastName.Value;
        e.BirthDate = entity.BirthDate;
        e.Gender = entity.Gender;
        e.AddressId = entity.AddressId;
        e.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PersonId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PersonEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PersonEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Person ToDomain(PersonEntity e)
    {
        return Person.Create(
            PersonId.Create(e.Id),
            PersonDocumentTypeRefId.Create(e.DocumentTypeId),
            PersonDocumentNumber.Create(e.DocumentNumber ?? string.Empty),
            PersonFirstName.Create(e.FirstName ?? string.Empty),
            PersonLastName.Create(e.LastName ?? string.Empty),
            e.BirthDate,
            e.Gender,
            e.AddressId,
            e.CreatedAt,
            e.UpdatedAt
        );
    }
}

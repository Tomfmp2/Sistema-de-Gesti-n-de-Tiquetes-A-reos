using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.repository;

public sealed class PassengerRepository : IPassengerRepository
{
    private readonly AppDbContext _context;

    public PassengerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Passenger?> GetByIdAsync(
        PassengerId id,
        CancellationToken cancellationToken = default
    )
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PassengerEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Passenger>> GetAllAsync(
        CancellationToken cancellationToken = default
    )
    {
        var list = await _context.Set<PassengerEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Passenger> AddAsync(Passenger entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Passenger.CreateNew para insertar.");
        }

        var e = new PassengerEntity
        {
            PersonId = entity.PersonId.Value,
            PassengerTypeId = entity.PassengerTypeId.Value,
        };
        _context.Set<PassengerEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Passenger entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PassengerEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe pasajero {entity.Id.Value}.");
        }

        e.PersonId = entity.PersonId.Value;
        e.PassengerTypeId = entity.PassengerTypeId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PassengerId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PassengerEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PassengerEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Passenger ToDomain(PassengerEntity e)
    {
        return Passenger.Create(
            PassengerId.Create(e.Id),
            PassengerPersonId.Create(e.PersonId),
            PassengerTypeRefId.Create(e.PassengerTypeId)
        );
    }
}

using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.repository;

public sealed class FareRepository : IFareRepository
{
    private readonly AppDbContext _context;

    public FareRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Fare?> GetByIdAsync(FareId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<FareEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Fare>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<FareEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Fare> AddAsync(Fare entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new FareEntity
        {
            RouteId = entity.RouteId.Value,
    CabinTypeId = entity.CabinTypeId.Value,
    PassengerTypeId = entity.PassengerTypeId.Value,
    SeasonId = entity.SeasonId.Value,
    BasePrice = entity.BasePrice.Value,
    ValidFrom = entity.ValidFrom.Value,
    ValidTo = entity.ValidTo.Value,
        };
        _context.Set<FareEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Fare entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<FareEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe fare {entity.Id.Value}.");
        }

        e.RouteId = entity.RouteId.Value;
e.CabinTypeId = entity.CabinTypeId.Value;
e.PassengerTypeId = entity.PassengerTypeId.Value;
e.SeasonId = entity.SeasonId.Value;
e.BasePrice = entity.BasePrice.Value;
e.ValidFrom = entity.ValidFrom.Value;
e.ValidTo = entity.ValidTo.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(FareId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<FareEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<FareEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Fare ToDomain(FareEntity e)
    {
        return Fare.Create(
            FareId.Create(e.Id),
    FareRouteId.Create(e.RouteId),
    FareCabinTypeId.Create(e.CabinTypeId),
    FarePassengerTypeId.Create(e.PassengerTypeId),
    FareSeasonId.Create(e.SeasonId),
    FareBasePrice.Create(e.BasePrice),
    FareValidFrom.Create(e.ValidFrom),
    FareValidTo.Create(e.ValidTo)
        );
    }
}

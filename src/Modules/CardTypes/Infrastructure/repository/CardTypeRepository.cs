using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.repository;

public sealed class CardTypeRepository : ICardTypeRepository
{
    private readonly AppDbContext _context;

    public CardTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CardType?> GetByIdAsync(CardTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<CardTypeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<CardType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<CardTypeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<CardType> AddAsync(CardType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new CardTypeEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<CardTypeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(CardType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<CardTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe cardtype {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CardTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<CardTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<CardTypeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static CardType ToDomain(CardTypeEntity e)
    {
        return CardType.Create(
            CardTypeId.Create(e.Id),
    CardTypeName.Create(e.Name)
        );
    }
}

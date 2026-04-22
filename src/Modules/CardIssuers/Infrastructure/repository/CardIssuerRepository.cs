using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.repository;

public sealed class CardIssuerRepository : ICardIssuerRepository
{
    private readonly AppDbContext _context;

    public CardIssuerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CardIssuer?> GetByIdAsync(CardIssuerId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<CardIssuerEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<CardIssuer>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<CardIssuerEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<CardIssuer> AddAsync(CardIssuer entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new CardIssuerEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<CardIssuerEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(CardIssuer entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<CardIssuerEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe cardissuer {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(CardIssuerId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<CardIssuerEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<CardIssuerEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static CardIssuer ToDomain(CardIssuerEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Name);
        return CardIssuer.Create(
            CardIssuerId.Create(e.Id),
    CardIssuerName.Create(e.Name)
        );
    }
}

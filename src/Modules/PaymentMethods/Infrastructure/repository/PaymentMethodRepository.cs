using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.repository;

public sealed class PaymentMethodRepository : IPaymentMethodRepository
{
    private readonly AppDbContext _context;

    public PaymentMethodRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentMethod?> GetByIdAsync(PaymentMethodId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PaymentMethodEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<PaymentMethodEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PaymentMethod> AddAsync(PaymentMethod entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new PaymentMethodEntity
        {
            PaymentMethodTypeId = entity.PaymentMethodTypeId.Value,
    CardTypeId = entity.CardTypeId.Value,
    CardIssuerId = entity.CardIssuerId.Value,
    CommercialName = entity.CommercialName.Value,
        };
        _context.Set<PaymentMethodEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PaymentMethod entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PaymentMethodEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe paymentmethod {entity.Id.Value}.");
        }

        e.PaymentMethodTypeId = entity.PaymentMethodTypeId.Value;
e.CardTypeId = entity.CardTypeId.Value;
e.CardIssuerId = entity.CardIssuerId.Value;
e.CommercialName = entity.CommercialName.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentMethodId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PaymentMethodEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PaymentMethodEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PaymentMethod ToDomain(PaymentMethodEntity e)
    {
        return PaymentMethod.Create(
            PaymentMethodId.Create(e.Id),
    PaymentMethodPaymentMethodTypeId.Create(e.PaymentMethodTypeId),
    PaymentMethodCardTypeId.Create(e.CardTypeId),
    PaymentMethodCardIssuerId.Create(e.CardIssuerId),
    PaymentMethodCommercialName.Create(e.CommercialName)
        );
    }
}

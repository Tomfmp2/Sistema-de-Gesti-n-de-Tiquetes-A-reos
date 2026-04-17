using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.repository;

public sealed class PaymentMethodTypeRepository : IPaymentMethodTypeRepository
{
    private readonly AppDbContext _context;

    public PaymentMethodTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentMethodType?> GetByIdAsync(PaymentMethodTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PaymentMethodTypeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PaymentMethodType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<PaymentMethodTypeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PaymentMethodType> AddAsync(PaymentMethodType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new PaymentMethodTypeEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<PaymentMethodTypeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PaymentMethodType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PaymentMethodTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe paymentmethodtype {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentMethodTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PaymentMethodTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PaymentMethodTypeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PaymentMethodType ToDomain(PaymentMethodTypeEntity e)
    {
        return PaymentMethodType.Create(
            PaymentMethodTypeId.Create(e.Id),
    PaymentMethodTypeName.Create(e.Name)
        );
    }
}

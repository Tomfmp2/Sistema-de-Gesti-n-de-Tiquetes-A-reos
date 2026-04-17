using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Infrastructure.repository;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PaymentEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<PaymentEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new PaymentEntity
        {
            ReservationId = entity.ReservationId.Value,
    Amount = entity.Amount.Value,
    PaymentDate = entity.PaymentDate.Value,
    PaymentStatusId = entity.PaymentStatusId.Value,
    PaymentMethodId = entity.PaymentMethodId.Value,
    CreatedAt = entity.CreatedAt.Value,
    UpdatedAt = entity.UpdatedAt.Value,
        };
        _context.Set<PaymentEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Payment entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PaymentEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe payment {entity.Id.Value}.");
        }

        e.ReservationId = entity.ReservationId.Value;
e.Amount = entity.Amount.Value;
e.PaymentDate = entity.PaymentDate.Value;
e.PaymentStatusId = entity.PaymentStatusId.Value;
e.PaymentMethodId = entity.PaymentMethodId.Value;
e.CreatedAt = entity.CreatedAt.Value;
e.UpdatedAt = entity.UpdatedAt.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PaymentEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PaymentEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Payment ToDomain(PaymentEntity e)
    {
        return Payment.Create(
            PaymentId.Create(e.Id),
    PaymentReservationId.Create(e.ReservationId),
    PaymentAmount.Create(e.Amount),
    PaymentDate.Create(e.PaymentDate),
    PaymentPaymentStatusId.Create(e.PaymentStatusId),
    PaymentPaymentMethodId.Create(e.PaymentMethodId),
    PaymentCreatedAt.Create(e.CreatedAt),
    PaymentUpdatedAt.Create(e.UpdatedAt)
        );
    }
}

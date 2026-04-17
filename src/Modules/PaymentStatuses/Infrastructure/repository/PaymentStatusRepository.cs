using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.repository;

public sealed class PaymentStatusRepository : IPaymentStatusRepository
{
    private readonly AppDbContext _context;

    public PaymentStatusRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentStatus?> GetByIdAsync(PaymentStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<PaymentStatusEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<PaymentStatusEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<PaymentStatus> AddAsync(PaymentStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new PaymentStatusEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<PaymentStatusEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(PaymentStatus entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<PaymentStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe paymentstatus {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(PaymentStatusId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<PaymentStatusEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<PaymentStatusEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static PaymentStatus ToDomain(PaymentStatusEntity e)
    {
        return PaymentStatus.Create(
            PaymentStatusId.Create(e.Id),
    PaymentStatusName.Create(e.Name)
        );
    }
}

using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Infrastructure.repository;

public sealed class InvoiceItemRepository : IInvoiceItemRepository
{
    private readonly AppDbContext _context;

    public InvoiceItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InvoiceItem?> GetByIdAsync(InvoiceItemId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<InvoiceItemEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<InvoiceItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<InvoiceItemEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<InvoiceItem> AddAsync(InvoiceItem entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new InvoiceItemEntity
        {
            InvoiceId = entity.InvoiceId.Value,
    InvoiceItemTypeId = entity.InvoiceItemTypeId.Value,
    Description = entity.Description.Value,
    Quantity = entity.Quantity.Value,
    UnitPrice = entity.UnitPrice.Value,
    Subtotal = entity.Subtotal.Value,
    ReservationPassengerId = entity.ReservationPassengerId.Value,
        };
        _context.Set<InvoiceItemEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(InvoiceItem entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<InvoiceItemEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe invoiceitem {entity.Id.Value}.");
        }

        e.InvoiceId = entity.InvoiceId.Value;
e.InvoiceItemTypeId = entity.InvoiceItemTypeId.Value;
e.Description = entity.Description.Value;
e.Quantity = entity.Quantity.Value;
e.UnitPrice = entity.UnitPrice.Value;
e.Subtotal = entity.Subtotal.Value;
e.ReservationPassengerId = entity.ReservationPassengerId.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InvoiceItemId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<InvoiceItemEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<InvoiceItemEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static InvoiceItem ToDomain(InvoiceItemEntity e)
    {
        return InvoiceItem.Create(
            InvoiceItemId.Create(e.Id),
    InvoiceItemInvoiceId.Create(e.InvoiceId),
    InvoiceItemTypeId.Create(e.InvoiceItemTypeId),
    InvoiceItemDescription.Create(e.Description),
    InvoiceItemQuantity.Create(e.Quantity),
    InvoiceItemUnitPrice.Create(e.UnitPrice),
    InvoiceItemSubtotal.Create(e.Subtotal),
    InvoiceItemReservationPassengerId.Create(e.ReservationPassengerId)
        );
    }
}

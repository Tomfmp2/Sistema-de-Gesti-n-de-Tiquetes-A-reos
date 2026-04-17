using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.repository;

public sealed class InvoiceRepository : IInvoiceRepository
{
    private readonly AppDbContext _context;

    public InvoiceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Invoice?> GetByIdAsync(InvoiceId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<InvoiceEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<InvoiceEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<Invoice> AddAsync(Invoice entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new InvoiceEntity
        {
            ReservationId = entity.ReservationId.Value,
    Number = entity.Number.Value,
    IssueDate = entity.IssueDate.Value,
    Subtotal = entity.Subtotal.Value,
    Taxes = entity.Taxes.Value,
    Total = entity.Total.Value,
    CreatedAt = entity.CreatedAt.Value,
        };
        _context.Set<InvoiceEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(Invoice entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<InvoiceEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe invoice {entity.Id.Value}.");
        }

        e.ReservationId = entity.ReservationId.Value;
e.Number = entity.Number.Value;
e.IssueDate = entity.IssueDate.Value;
e.Subtotal = entity.Subtotal.Value;
e.Taxes = entity.Taxes.Value;
e.Total = entity.Total.Value;
e.CreatedAt = entity.CreatedAt.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InvoiceId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<InvoiceEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<InvoiceEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static Invoice ToDomain(InvoiceEntity e)
    {
        return Invoice.Create(
            InvoiceId.Create(e.Id),
    InvoiceReservationId.Create(e.ReservationId),
    InvoiceNumber.Create(e.Number),
    InvoiceIssueDate.Create(e.IssueDate),
    InvoiceSubtotal.Create(e.Subtotal),
    InvoiceTaxes.Create(e.Taxes),
    InvoiceTotal.Create(e.Total),
    InvoiceCreatedAt.Create(e.CreatedAt)
        );
    }
}

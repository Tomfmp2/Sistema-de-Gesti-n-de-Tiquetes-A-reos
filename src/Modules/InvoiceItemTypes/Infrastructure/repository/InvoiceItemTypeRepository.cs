using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.repository;

public sealed class InvoiceItemTypeRepository : IInvoiceItemTypeRepository
{
    private readonly AppDbContext _context;

    public InvoiceItemTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<InvoiceItemType?> GetByIdAsync(InvoiceItemTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return null;
        }

        var e = await _context.Set<InvoiceItemTypeEntity>().AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        return e is null ? null : ToDomain(e);
    }

    public async Task<IReadOnlyList<InvoiceItemType>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var list = await _context.Set<InvoiceItemTypeEntity>().AsNoTracking().ToListAsync(cancellationToken);
        return list.Select(ToDomain).ToList();
    }

    public async Task<InvoiceItemType> AddAsync(InvoiceItemType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value != 0)
        {
            throw new InvalidOperationException("Use Id en 0 para insertar.");
        }

        var e = new InvoiceItemTypeEntity
        {
            Name = entity.Name.Value,
        };
        _context.Set<InvoiceItemTypeEntity>().Add(e);
        await _context.SaveChangesAsync(cancellationToken);
        return ToDomain(e);
    }

    public async Task UpdateAsync(InvoiceItemType entity, CancellationToken cancellationToken = default)
    {
        if (entity.Id.Value < 1)
        {
            throw new InvalidOperationException("Id inválido.");
        }

        var e = await _context.Set<InvoiceItemTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == entity.Id.Value,
            cancellationToken
        );

        if (e is null)
        {
            throw new InvalidOperationException($"No existe invoiceitemtype {entity.Id.Value}.");
        }

        e.Name = entity.Name.Value;
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(InvoiceItemTypeId id, CancellationToken cancellationToken = default)
    {
        if (id.Value < 1)
        {
            return;
        }

        var e = await _context.Set<InvoiceItemTypeEntity>().FirstOrDefaultAsync(
            x => x.Id == id.Value,
            cancellationToken
        );

        if (e is null)
        {
            return;
        }

        _context.Set<InvoiceItemTypeEntity>().Remove(e);
        await _context.SaveChangesAsync(cancellationToken);
    }

    private static InvoiceItemType ToDomain(InvoiceItemTypeEntity e)
    {
        ArgumentNullException.ThrowIfNull(e.Name);
        return InvoiceItemType.Create(
            InvoiceItemTypeId.Create(e.Id),
    InvoiceItemTypeName.Create(e.Name)
        );
    }
}

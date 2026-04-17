using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;

public interface IInvoiceItemRepository
{
    Task<InvoiceItem?> GetByIdAsync(InvoiceItemId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InvoiceItem>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InvoiceItem> AddAsync(InvoiceItem entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(InvoiceItem entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(InvoiceItemId id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;

public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(InvoiceId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Invoice> AddAsync(Invoice entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Invoice entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(InvoiceId id, CancellationToken cancellationToken = default);
}

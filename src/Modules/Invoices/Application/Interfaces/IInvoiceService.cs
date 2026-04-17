using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Interfaces;

public interface IInvoiceService
{
    Task<Invoice> CreateAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateInvoiceRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

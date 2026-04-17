using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Interfaces;

public interface IInvoiceItemService
{
    Task<InvoiceItem> CreateAsync(
        CreateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    );

    Task<InvoiceItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<InvoiceItem>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

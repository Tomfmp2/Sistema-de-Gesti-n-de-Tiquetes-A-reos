using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Interfaces;

public interface IInvoiceItemTypeService
{
    Task<InvoiceItemType> CreateAsync(
        CreateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<InvoiceItemType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<InvoiceItemType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;

public interface IInvoiceItemTypeRepository
{
    Task<InvoiceItemType?> GetByIdAsync(InvoiceItemTypeId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<InvoiceItemType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<InvoiceItemType> AddAsync(InvoiceItemType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(InvoiceItemType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(InvoiceItemTypeId id, CancellationToken cancellationToken = default);
}

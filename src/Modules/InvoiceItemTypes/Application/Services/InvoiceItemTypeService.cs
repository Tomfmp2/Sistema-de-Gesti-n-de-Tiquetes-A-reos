using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Services;

public sealed class InvoiceItemTypeService : IInvoiceItemTypeService
{
    private readonly ICreateInvoiceItemTypeUseCase _create;
    private readonly IGetInvoiceItemTypeByIdUseCase _getById;
    private readonly IGetAllInvoiceItemTypesUseCase _getAll;
    private readonly IUpdateInvoiceItemTypeUseCase _update;
    private readonly IDeleteInvoiceItemTypeUseCase _delete;

    public InvoiceItemTypeService(
        ICreateInvoiceItemTypeUseCase create,
        IGetInvoiceItemTypeByIdUseCase getById,
        IGetAllInvoiceItemTypesUseCase getAll,
        IUpdateInvoiceItemTypeUseCase update,
        IDeleteInvoiceItemTypeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<InvoiceItemType> CreateAsync(
        CreateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<InvoiceItemType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<InvoiceItemType>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

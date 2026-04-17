using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Services;

public sealed class InvoiceItemService : IInvoiceItemService
{
    private readonly ICreateInvoiceItemUseCase _create;
    private readonly IGetInvoiceItemByIdUseCase _getById;
    private readonly IGetAllInvoiceItemsUseCase _getAll;
    private readonly IUpdateInvoiceItemUseCase _update;
    private readonly IDeleteInvoiceItemUseCase _delete;

    public InvoiceItemService(
        ICreateInvoiceItemUseCase create,
        IGetInvoiceItemByIdUseCase getById,
        IGetAllInvoiceItemsUseCase getAll,
        IUpdateInvoiceItemUseCase update,
        IDeleteInvoiceItemUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<InvoiceItem> CreateAsync(
        CreateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<InvoiceItem?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<InvoiceItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

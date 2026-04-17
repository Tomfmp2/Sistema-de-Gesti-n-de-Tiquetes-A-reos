using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Services;

public sealed class InvoiceService : IInvoiceService
{
    private readonly ICreateInvoiceUseCase _create;
    private readonly IGetInvoiceByIdUseCase _getById;
    private readonly IGetAllInvoicesUseCase _getAll;
    private readonly IUpdateInvoiceUseCase _update;
    private readonly IDeleteInvoiceUseCase _delete;

    public InvoiceService(
        ICreateInvoiceUseCase create,
        IGetInvoiceByIdUseCase getById,
        IGetAllInvoicesUseCase getAll,
        IUpdateInvoiceUseCase update,
        IDeleteInvoiceUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Invoice> CreateAsync(
        CreateInvoiceRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Invoice?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateInvoiceRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

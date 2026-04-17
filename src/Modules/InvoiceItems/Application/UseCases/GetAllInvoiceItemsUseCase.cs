using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.UseCases;

public interface IGetAllInvoiceItemsUseCase
{
    Task<IReadOnlyList<InvoiceItem>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllInvoiceItemsUseCase : IGetAllInvoiceItemsUseCase
{
    private readonly IInvoiceItemRepository _repository;

    public GetAllInvoiceItemsUseCase(IInvoiceItemRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<InvoiceItem>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.UseCases;

public interface IGetAllInvoiceItemTypesUseCase
{
    Task<IReadOnlyList<InvoiceItemType>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllInvoiceItemTypesUseCase : IGetAllInvoiceItemTypesUseCase
{
    private readonly IInvoiceItemTypeRepository _repository;

    public GetAllInvoiceItemTypesUseCase(IInvoiceItemTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<InvoiceItemType>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

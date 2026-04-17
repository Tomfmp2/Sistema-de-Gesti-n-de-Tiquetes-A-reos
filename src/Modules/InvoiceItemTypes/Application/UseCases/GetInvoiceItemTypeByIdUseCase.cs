using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.UseCases;

public interface IGetInvoiceItemTypeByIdUseCase
{
    Task<InvoiceItemType?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetInvoiceItemTypeByIdUseCase : IGetInvoiceItemTypeByIdUseCase
{
    private readonly IInvoiceItemTypeRepository _repository;

    public GetInvoiceItemTypeByIdUseCase(IInvoiceItemTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<InvoiceItemType?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<InvoiceItemType?>(null);
        }

        return _repository.GetByIdAsync(InvoiceItemTypeId.Create(id), cancellationToken);
    }
}

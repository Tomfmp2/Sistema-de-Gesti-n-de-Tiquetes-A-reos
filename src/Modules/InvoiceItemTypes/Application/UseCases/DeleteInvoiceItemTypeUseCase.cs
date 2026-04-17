using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.UseCases;

public interface IDeleteInvoiceItemTypeUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteInvoiceItemTypeUseCase : IDeleteInvoiceItemTypeUseCase
{
    private readonly IInvoiceItemTypeRepository _repository;

    public DeleteInvoiceItemTypeUseCase(IInvoiceItemTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(InvoiceItemTypeId.Create(id), cancellationToken);
    }
}

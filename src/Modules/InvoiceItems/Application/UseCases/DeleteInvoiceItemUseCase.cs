using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.UseCases;

public interface IDeleteInvoiceItemUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteInvoiceItemUseCase : IDeleteInvoiceItemUseCase
{
    private readonly IInvoiceItemRepository _repository;

    public DeleteInvoiceItemUseCase(IInvoiceItemRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(InvoiceItemId.Create(id), cancellationToken);
    }
}

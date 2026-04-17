using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;

public interface IDeleteInvoiceUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteInvoiceUseCase : IDeleteInvoiceUseCase
{
    private readonly IInvoiceRepository _repository;

    public DeleteInvoiceUseCase(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(InvoiceId.Create(id), cancellationToken);
    }
}

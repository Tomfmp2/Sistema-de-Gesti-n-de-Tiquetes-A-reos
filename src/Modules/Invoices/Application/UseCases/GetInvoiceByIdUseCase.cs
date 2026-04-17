using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;

public interface IGetInvoiceByIdUseCase
{
    Task<Invoice?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetInvoiceByIdUseCase : IGetInvoiceByIdUseCase
{
    private readonly IInvoiceRepository _repository;

    public GetInvoiceByIdUseCase(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Task<Invoice?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Invoice?>(null);
        }

        return _repository.GetByIdAsync(InvoiceId.Create(id), cancellationToken);
    }
}

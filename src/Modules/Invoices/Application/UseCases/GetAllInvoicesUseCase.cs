using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.UseCases;

public interface IGetAllInvoicesUseCase
{
    Task<IReadOnlyList<Invoice>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllInvoicesUseCase : IGetAllInvoicesUseCase
{
    private readonly IInvoiceRepository _repository;

    public GetAllInvoicesUseCase(IInvoiceRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Invoice>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

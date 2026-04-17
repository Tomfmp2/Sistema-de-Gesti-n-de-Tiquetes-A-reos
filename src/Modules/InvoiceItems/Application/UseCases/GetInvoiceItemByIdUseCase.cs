using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.UseCases;

public interface IGetInvoiceItemByIdUseCase
{
    Task<InvoiceItem?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetInvoiceItemByIdUseCase : IGetInvoiceItemByIdUseCase
{
    private readonly IInvoiceItemRepository _repository;

    public GetInvoiceItemByIdUseCase(IInvoiceItemRepository repository)
    {
        _repository = repository;
    }

    public Task<InvoiceItem?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<InvoiceItem?>(null);
        }

        return _repository.GetByIdAsync(InvoiceItemId.Create(id), cancellationToken);
    }
}

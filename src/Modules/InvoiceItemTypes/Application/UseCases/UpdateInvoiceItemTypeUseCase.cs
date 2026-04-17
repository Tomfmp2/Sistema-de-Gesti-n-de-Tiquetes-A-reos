using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.UseCases;

public interface IUpdateInvoiceItemTypeUseCase
{
    Task ExecuteAsync(
        UpdateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateInvoiceItemTypeUseCase : IUpdateInvoiceItemTypeUseCase
{
    private readonly IInvoiceItemTypeRepository _repository;

    public UpdateInvoiceItemTypeUseCase(IInvoiceItemTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = InvoiceItemType.Create(InvoiceItemTypeId.Create(request.Id), InvoiceItemTypeName.Create(request.Name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

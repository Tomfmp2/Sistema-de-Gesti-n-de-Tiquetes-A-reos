using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.UseCases;

public interface ICreateInvoiceItemTypeUseCase
{
    Task<InvoiceItemType> ExecuteAsync(
        CreateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateInvoiceItemTypeUseCase : ICreateInvoiceItemTypeUseCase
{
    private readonly IInvoiceItemTypeRepository _repository;

    public CreateInvoiceItemTypeUseCase(IInvoiceItemTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<InvoiceItemType> ExecuteAsync(
        CreateInvoiceItemTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = InvoiceItemType.Create(new InvoiceItemTypeId(0), InvoiceItemTypeName.Create(name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

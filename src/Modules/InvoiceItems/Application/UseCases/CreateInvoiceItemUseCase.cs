using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.UseCases;

public interface ICreateInvoiceItemUseCase
{
    Task<InvoiceItem> ExecuteAsync(
        CreateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateInvoiceItemUseCase : ICreateInvoiceItemUseCase
{
    private readonly IInvoiceItemRepository _repository;

    public CreateInvoiceItemUseCase(IInvoiceItemRepository repository)
    {
        _repository = repository;
    }

    public Task<InvoiceItem> ExecuteAsync(
        CreateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = InvoiceItem.Create(new InvoiceItemId(0), InvoiceItemInvoiceId.Create(request.InvoiceId), InvoiceItemTypeId.Create(request.InvoiceItemTypeId), InvoiceItemDescription.Create(request.Description), InvoiceItemQuantity.Create(request.Quantity), InvoiceItemUnitPrice.Create(request.UnitPrice), InvoiceItemSubtotal.Create(request.Subtotal), InvoiceItemReservationPassengerId.Create(request.ReservationPassengerId));
        return _repository.AddAsync(x, cancellationToken);
    }
}

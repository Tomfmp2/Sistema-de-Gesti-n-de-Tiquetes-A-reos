using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.UseCases;

public interface IUpdateInvoiceItemUseCase
{
    Task ExecuteAsync(
        UpdateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateInvoiceItemUseCase : IUpdateInvoiceItemUseCase
{
    private readonly IInvoiceItemRepository _repository;

    public UpdateInvoiceItemUseCase(IInvoiceItemRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateInvoiceItemRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var description = request.Description;
        ArgumentNullException.ThrowIfNull(description);
        var x = InvoiceItem.Create(InvoiceItemId.Create(request.Id), InvoiceItemInvoiceId.Create(request.InvoiceId), InvoiceItemTypeId.Create(request.InvoiceItemTypeId), InvoiceItemDescription.Create(description), InvoiceItemQuantity.Create(request.Quantity), InvoiceItemUnitPrice.Create(request.UnitPrice), InvoiceItemSubtotal.Create(request.Subtotal), InvoiceItemReservationPassengerId.Create(request.ReservationPassengerId));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

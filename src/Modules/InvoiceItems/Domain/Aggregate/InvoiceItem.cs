using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Domain.Aggregate;

public class InvoiceItem
{
    public InvoiceItemId Id { get; private set; }
    public InvoiceItemInvoiceId InvoiceId { get; private set; }
    public InvoiceItemTypeId InvoiceItemTypeId { get; private set; }
    public InvoiceItemDescription Description { get; private set; }
    public InvoiceItemQuantity Quantity { get; private set; }
    public InvoiceItemUnitPrice UnitPrice { get; private set; }
    public InvoiceItemSubtotal Subtotal { get; private set; }
    public InvoiceItemReservationPassengerId ReservationPassengerId { get; private set; }

    private InvoiceItem(
        InvoiceItemId id,
        InvoiceItemInvoiceId invoiceId,
        InvoiceItemTypeId invoiceItemTypeId,
        InvoiceItemDescription description,
        InvoiceItemQuantity quantity,
        InvoiceItemUnitPrice unitPrice,
        InvoiceItemSubtotal subtotal,
        InvoiceItemReservationPassengerId reservationPassengerId
    )
    {
        Id = id;
        InvoiceId = invoiceId;
        InvoiceItemTypeId = invoiceItemTypeId;
        Description = description;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = subtotal;
        ReservationPassengerId = reservationPassengerId;
    }

    public static InvoiceItem Create(
        InvoiceItemId id,
        InvoiceItemInvoiceId invoiceId,
        InvoiceItemTypeId invoiceItemTypeId,
        InvoiceItemDescription description,
        InvoiceItemQuantity quantity,
        InvoiceItemUnitPrice unitPrice,
        InvoiceItemSubtotal subtotal,
        InvoiceItemReservationPassengerId reservationPassengerId
    )
    {
        return new InvoiceItem(
            id,
            invoiceId,
            invoiceItemTypeId,
            description,
            quantity,
            unitPrice,
            subtotal,
            reservationPassengerId
        );
    }
}

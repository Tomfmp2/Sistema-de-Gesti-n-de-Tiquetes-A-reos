using sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Domain.Aggregate;

public class Invoice
{
    public InvoiceId Id { get; private set; }
    public InvoiceReservationId ReservationId { get; private set; }
    public InvoiceNumber Number { get; private set; }
    public InvoiceIssueDate IssueDate { get; private set; }
    public InvoiceSubtotal Subtotal { get; private set; }
    public InvoiceTaxes Taxes { get; private set; }
    public InvoiceTotal Total { get; private set; }
    public InvoiceCreatedAt CreatedAt { get; private set; }

    private Invoice(
        InvoiceId id,
        InvoiceReservationId reservationId,
        InvoiceNumber number,
        InvoiceIssueDate issueDate,
        InvoiceSubtotal subtotal,
        InvoiceTaxes taxes,
        InvoiceTotal total,
        InvoiceCreatedAt createdAt
    )
    {
        Id = id;
        ReservationId = reservationId;
        Number = number;
        IssueDate = issueDate;
        Subtotal = subtotal;
        Taxes = taxes;
        Total = total;
        CreatedAt = createdAt;
    }

    public static Invoice Create(
        InvoiceId id,
        InvoiceReservationId reservationId,
        InvoiceNumber number,
        InvoiceIssueDate issueDate,
        InvoiceSubtotal subtotal,
        InvoiceTaxes taxes,
        InvoiceTotal total,
        InvoiceCreatedAt createdAt
    )
    {
        return new Invoice(
            id,
            reservationId,
            number,
            issueDate,
            subtotal,
            taxes,
            total,
            createdAt
        );
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Infrastructure.Entity;

public class InvoiceEntity
{
    public int Id { get; set; }
    public int ReservationId { get; set; }
    public string? Number { get; set; }
    public DateTime IssueDate { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Taxes { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAt { get; set; }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Invoices.Application.Dtos;

public sealed record CreateInvoiceRequest(int ReservationId, string? Number, DateTime IssueDate, decimal Subtotal, decimal Taxes, decimal Total, DateTime CreatedAt);

public sealed record UpdateInvoiceRequest(int Id, int ReservationId, string? Number, DateTime IssueDate, decimal Subtotal, decimal Taxes, decimal Total, DateTime CreatedAt);

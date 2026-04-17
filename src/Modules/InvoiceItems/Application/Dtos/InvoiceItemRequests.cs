namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItems.Application.Dtos;

public sealed record CreateInvoiceItemRequest(int InvoiceId, int InvoiceItemTypeId, string? Description, int Quantity, decimal UnitPrice, decimal Subtotal, int? ReservationPassengerId);

public sealed record UpdateInvoiceItemRequest(int Id, int InvoiceId, int InvoiceItemTypeId, string? Description, int Quantity, decimal UnitPrice, decimal Subtotal, int? ReservationPassengerId);

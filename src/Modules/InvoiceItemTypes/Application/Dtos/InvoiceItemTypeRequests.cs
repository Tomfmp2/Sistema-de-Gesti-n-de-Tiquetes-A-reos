namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Application.Dtos;

public sealed record CreateInvoiceItemTypeRequest(string? Name);

public sealed record UpdateInvoiceItemTypeRequest(int Id, string? Name);

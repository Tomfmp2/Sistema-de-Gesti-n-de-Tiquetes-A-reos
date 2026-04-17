namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Dtos;

public sealed record CreatePaymentStatusRequest(string? Name);

public sealed record UpdatePaymentStatusRequest(int Id, string? Name);

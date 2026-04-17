namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Dtos;

public sealed record CreatePaymentMethodTypeRequest(string? Name);

public sealed record UpdatePaymentMethodTypeRequest(int Id, string? Name);

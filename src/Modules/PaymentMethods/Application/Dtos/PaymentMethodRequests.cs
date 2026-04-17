namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Dtos;

public sealed record CreatePaymentMethodRequest(int PaymentMethodTypeId, int? CardTypeId, int? CardIssuerId, string? CommercialName);

public sealed record UpdatePaymentMethodRequest(int Id, int PaymentMethodTypeId, int? CardTypeId, int? CardIssuerId, string? CommercialName);

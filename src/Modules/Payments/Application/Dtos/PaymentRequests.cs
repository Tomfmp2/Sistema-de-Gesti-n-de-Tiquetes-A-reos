namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Dtos;

public sealed record CreatePaymentRequest(int ReservationId, decimal Amount, DateTime PaymentDate, int PaymentStatusId, int PaymentMethodId, DateTime CreatedAt, DateTime UpdatedAt);

public sealed record UpdatePaymentRequest(int Id, int ReservationId, decimal Amount, DateTime PaymentDate, int PaymentStatusId, int PaymentMethodId, DateTime CreatedAt, DateTime UpdatedAt);

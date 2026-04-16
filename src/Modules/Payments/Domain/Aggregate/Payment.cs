using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;

public class Payment
{
    public PaymentId Id { get; private set; }
    public PaymentReservationId ReservationId { get; private set; }
    public PaymentAmount Amount { get; private set; }
    public PaymentDate PaymentDate { get; private set; }
    public PaymentPaymentStatusId PaymentStatusId { get; private set; }
    public PaymentPaymentMethodId PaymentMethodId { get; private set; }
    public PaymentCreatedAt CreatedAt { get; private set; }
    public PaymentUpdatedAt UpdatedAt { get; private set; }

    private Payment(
        PaymentId id,
        PaymentReservationId reservationId,
        PaymentAmount amount,
        PaymentDate paymentDate,
        PaymentPaymentStatusId paymentStatusId,
        PaymentPaymentMethodId paymentMethodId,
        PaymentCreatedAt createdAt,
        PaymentUpdatedAt updatedAt
    )
    {
        Id = id;
        ReservationId = reservationId;
        Amount = amount;
        PaymentDate = paymentDate;
        PaymentStatusId = paymentStatusId;
        PaymentMethodId = paymentMethodId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Payment Create(
        PaymentId id,
        PaymentReservationId reservationId,
        PaymentAmount amount,
        PaymentDate paymentDate,
        PaymentPaymentStatusId paymentStatusId,
        PaymentPaymentMethodId paymentMethodId,
        PaymentCreatedAt createdAt,
        PaymentUpdatedAt updatedAt
    )
    {
        return new Payment(
            id,
            reservationId,
            amount,
            paymentDate,
            paymentStatusId,
            paymentMethodId,
            createdAt,
            updatedAt
        );
    }
}

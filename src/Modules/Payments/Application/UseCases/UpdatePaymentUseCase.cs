using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;

public interface IUpdatePaymentUseCase
{
    Task ExecuteAsync(
        UpdatePaymentRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePaymentUseCase : IUpdatePaymentUseCase
{
    private readonly IPaymentRepository _repository;

    public UpdatePaymentUseCase(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePaymentRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Payment.Create(PaymentId.Create(request.Id), PaymentReservationId.Create(request.ReservationId), PaymentAmount.Create(request.Amount), PaymentDate.Create(request.PaymentDate), PaymentPaymentStatusId.Create(request.PaymentStatusId), PaymentPaymentMethodId.Create(request.PaymentMethodId), PaymentCreatedAt.Create(request.CreatedAt), PaymentUpdatedAt.Create(request.UpdatedAt));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

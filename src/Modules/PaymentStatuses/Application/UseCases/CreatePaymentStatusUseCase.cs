using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.UseCases;

public interface ICreatePaymentStatusUseCase
{
    Task<PaymentStatus> ExecuteAsync(
        CreatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePaymentStatusUseCase : ICreatePaymentStatusUseCase
{
    private readonly IPaymentStatusRepository _repository;

    public CreatePaymentStatusUseCase(IPaymentStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentStatus> ExecuteAsync(
        CreatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PaymentStatus.Create(new PaymentStatusId(0), PaymentStatusName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

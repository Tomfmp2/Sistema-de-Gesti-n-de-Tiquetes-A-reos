using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.UseCases;

public interface IUpdatePaymentStatusUseCase
{
    Task ExecuteAsync(
        UpdatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePaymentStatusUseCase : IUpdatePaymentStatusUseCase
{
    private readonly IPaymentStatusRepository _repository;

    public UpdatePaymentStatusUseCase(IPaymentStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = PaymentStatus.Create(PaymentStatusId.Create(request.Id), PaymentStatusName.Create(name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

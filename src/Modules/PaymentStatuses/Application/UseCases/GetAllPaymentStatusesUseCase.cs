using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.UseCases;

public interface IGetAllPaymentStatusesUseCase
{
    Task<IReadOnlyList<PaymentStatus>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPaymentStatusesUseCase : IGetAllPaymentStatusesUseCase
{
    private readonly IPaymentStatusRepository _repository;

    public GetAllPaymentStatusesUseCase(IPaymentStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PaymentStatus>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

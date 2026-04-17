using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;

public interface IGetAllPaymentsUseCase
{
    Task<IReadOnlyList<Payment>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPaymentsUseCase : IGetAllPaymentsUseCase
{
    private readonly IPaymentRepository _repository;

    public GetAllPaymentsUseCase(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Payment>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

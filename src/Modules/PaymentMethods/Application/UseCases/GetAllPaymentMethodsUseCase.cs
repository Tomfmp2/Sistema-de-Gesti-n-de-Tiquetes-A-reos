using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.UseCases;

public interface IGetAllPaymentMethodsUseCase
{
    Task<IReadOnlyList<PaymentMethod>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPaymentMethodsUseCase : IGetAllPaymentMethodsUseCase
{
    private readonly IPaymentMethodRepository _repository;

    public GetAllPaymentMethodsUseCase(IPaymentMethodRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PaymentMethod>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

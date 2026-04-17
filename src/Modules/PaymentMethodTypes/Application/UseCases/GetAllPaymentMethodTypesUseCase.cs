using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.UseCases;

public interface IGetAllPaymentMethodTypesUseCase
{
    Task<IReadOnlyList<PaymentMethodType>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPaymentMethodTypesUseCase : IGetAllPaymentMethodTypesUseCase
{
    private readonly IPaymentMethodTypeRepository _repository;

    public GetAllPaymentMethodTypesUseCase(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PaymentMethodType>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

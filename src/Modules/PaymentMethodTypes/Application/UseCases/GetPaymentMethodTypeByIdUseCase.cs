using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.UseCases;

public interface IGetPaymentMethodTypeByIdUseCase
{
    Task<PaymentMethodType?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPaymentMethodTypeByIdUseCase : IGetPaymentMethodTypeByIdUseCase
{
    private readonly IPaymentMethodTypeRepository _repository;

    public GetPaymentMethodTypeByIdUseCase(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentMethodType?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PaymentMethodType?>(null);
        }

        return _repository.GetByIdAsync(PaymentMethodTypeId.Create(id), cancellationToken);
    }
}

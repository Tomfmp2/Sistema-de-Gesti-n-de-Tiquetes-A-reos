using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.UseCases;

public interface IGetPaymentMethodByIdUseCase
{
    Task<PaymentMethod?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPaymentMethodByIdUseCase : IGetPaymentMethodByIdUseCase
{
    private readonly IPaymentMethodRepository _repository;

    public GetPaymentMethodByIdUseCase(IPaymentMethodRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentMethod?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PaymentMethod?>(null);
        }

        return _repository.GetByIdAsync(PaymentMethodId.Create(id), cancellationToken);
    }
}

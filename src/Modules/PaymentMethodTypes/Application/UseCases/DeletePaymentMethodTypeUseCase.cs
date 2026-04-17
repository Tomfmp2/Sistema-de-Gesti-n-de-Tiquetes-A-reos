using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.UseCases;

public interface IDeletePaymentMethodTypeUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePaymentMethodTypeUseCase : IDeletePaymentMethodTypeUseCase
{
    private readonly IPaymentMethodTypeRepository _repository;

    public DeletePaymentMethodTypeUseCase(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PaymentMethodTypeId.Create(id), cancellationToken);
    }
}

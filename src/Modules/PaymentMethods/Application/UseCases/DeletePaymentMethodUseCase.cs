using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.UseCases;

public interface IDeletePaymentMethodUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePaymentMethodUseCase : IDeletePaymentMethodUseCase
{
    private readonly IPaymentMethodRepository _repository;

    public DeletePaymentMethodUseCase(IPaymentMethodRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PaymentMethodId.Create(id), cancellationToken);
    }
}

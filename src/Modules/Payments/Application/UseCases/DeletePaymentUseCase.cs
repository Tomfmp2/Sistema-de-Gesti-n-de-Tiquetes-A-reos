using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;

public interface IDeletePaymentUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePaymentUseCase : IDeletePaymentUseCase
{
    private readonly IPaymentRepository _repository;

    public DeletePaymentUseCase(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PaymentId.Create(id), cancellationToken);
    }
}

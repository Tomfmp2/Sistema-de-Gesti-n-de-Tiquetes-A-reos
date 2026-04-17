using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.UseCases;

public interface IDeletePaymentStatusUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeletePaymentStatusUseCase : IDeletePaymentStatusUseCase
{
    private readonly IPaymentStatusRepository _repository;

    public DeletePaymentStatusUseCase(IPaymentStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(PaymentStatusId.Create(id), cancellationToken);
    }
}

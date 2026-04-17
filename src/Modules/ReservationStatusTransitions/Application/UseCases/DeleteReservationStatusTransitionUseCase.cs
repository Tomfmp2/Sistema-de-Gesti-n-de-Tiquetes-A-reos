using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.UseCases;

public interface IDeleteReservationStatusTransitionUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteReservationStatusTransitionUseCase : IDeleteReservationStatusTransitionUseCase
{
    private readonly IReservationStatusTransitionRepository _repository;

    public DeleteReservationStatusTransitionUseCase(IReservationStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(ReservationStatusTransitionId.Create(id), cancellationToken);
    }
}

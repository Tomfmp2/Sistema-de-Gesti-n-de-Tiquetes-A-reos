using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.UseCases;

public interface IDeleteReservationStatusUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteReservationStatusUseCase : IDeleteReservationStatusUseCase
{
    private readonly IReservationStatusRepository _repository;

    public DeleteReservationStatusUseCase(IReservationStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(ReservationStatusId.Create(id), cancellationToken);
    }
}

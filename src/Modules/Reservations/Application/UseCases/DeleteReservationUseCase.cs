using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;

public interface IDeleteReservationUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteReservationUseCase : IDeleteReservationUseCase
{
    private readonly IReservationRepository _repository;

    public DeleteReservationUseCase(IReservationRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(ReservationId.Create(id), cancellationToken);
    }
}

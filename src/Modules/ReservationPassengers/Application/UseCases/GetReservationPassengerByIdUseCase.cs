using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;

public interface IGetReservationPassengerByIdUseCase
{
    Task<ReservationPassenger?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetReservationPassengerByIdUseCase : IGetReservationPassengerByIdUseCase
{
    private readonly IReservationPassengerRepository _repository;

    public GetReservationPassengerByIdUseCase(IReservationPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationPassenger?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<ReservationPassenger?>(null);
        }

        return _repository.GetByIdAsync(ReservationPassengerId.Create(id), cancellationToken);
    }
}

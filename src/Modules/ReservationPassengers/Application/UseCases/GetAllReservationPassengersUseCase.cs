using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;

public interface IGetAllReservationPassengersUseCase
{
    Task<IReadOnlyList<ReservationPassenger>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllReservationPassengersUseCase : IGetAllReservationPassengersUseCase
{
    private readonly IReservationPassengerRepository _repository;

    public GetAllReservationPassengersUseCase(IReservationPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<ReservationPassenger>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;

public interface IUpdateReservationPassengerUseCase
{
    Task ExecuteAsync(
        UpdateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateReservationPassengerUseCase : IUpdateReservationPassengerUseCase
{
    private readonly IReservationPassengerRepository _repository;

    public UpdateReservationPassengerUseCase(IReservationPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationPassenger.Create(
            ReservationPassengerId.Create(request.Id),
            ReservationPassengerReservationFlightId.Create(request.ReservationFlightId),
            ReservationPassengerPassengerId.Create(request.PassengerId),
            ReservationPassengerCabinTypeId.Create(request.CabinTypeId)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Application.UseCases;

public interface ICreateReservationPassengerUseCase
{
    Task<ReservationPassenger> ExecuteAsync(
        CreateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateReservationPassengerUseCase : ICreateReservationPassengerUseCase
{
    private readonly IReservationPassengerRepository _repository;

    public CreateReservationPassengerUseCase(IReservationPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationPassenger> ExecuteAsync(
        CreateReservationPassengerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationPassenger.Create(new ReservationPassengerId(0), ReservationPassengerReservationFlightId.Create(request.ReservationFlightId), ReservationPassengerPassengerId.Create(request.PassengerId));
        return _repository.AddAsync(x, cancellationToken);
    }
}

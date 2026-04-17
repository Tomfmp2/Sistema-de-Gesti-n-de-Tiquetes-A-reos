using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;

public interface IUpdatePassengerUseCase
{
    Task ExecuteAsync(
        UpdatePassengerRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePassengerUseCase : IUpdatePassengerUseCase
{
    private readonly IPassengerRepository _repository;

    public UpdatePassengerUseCase(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePassengerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Passenger.Create(
            PassengerId.Create(request.Id),
            PassengerPersonId.Create(request.PersonId),
            PassengerTypeRefId.Create(request.PassengerTypeId)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

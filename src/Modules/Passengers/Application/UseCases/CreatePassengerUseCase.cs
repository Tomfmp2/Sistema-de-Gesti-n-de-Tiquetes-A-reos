using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;

public interface ICreatePassengerUseCase
{
    Task<Passenger> ExecuteAsync(
        CreatePassengerRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePassengerUseCase : ICreatePassengerUseCase
{
    private readonly IPassengerRepository _repository;

    public CreatePassengerUseCase(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task<Passenger> ExecuteAsync(
        CreatePassengerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Passenger.CreateNew(
            PassengerPersonId.Create(request.PersonId),
            PassengerTypeRefId.Create(request.PassengerTypeId)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;

public interface IGetPassengerByIdUseCase
{
    Task<Passenger?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPassengerByIdUseCase : IGetPassengerByIdUseCase
{
    private readonly IPassengerRepository _repository;

    public GetPassengerByIdUseCase(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task<Passenger?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Passenger?>(null);
        }

        return _repository.GetByIdAsync(PassengerId.Create(id), cancellationToken);
    }
}

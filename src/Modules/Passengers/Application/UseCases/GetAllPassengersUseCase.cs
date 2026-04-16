using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;

public interface IGetAllPassengersUseCase
{
    Task<IReadOnlyList<Passenger>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPassengersUseCase : IGetAllPassengersUseCase
{
    private readonly IPassengerRepository _repository;

    public GetAllPassengersUseCase(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Passenger>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

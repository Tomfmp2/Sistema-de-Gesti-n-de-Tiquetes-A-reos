using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;

public interface IGetAllFlightsUseCase
{
    Task<IReadOnlyList<Flight>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllFlightsUseCase : IGetAllFlightsUseCase
{
    private readonly IFlightRepository _repository;

    public GetAllFlightsUseCase(IFlightRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Flight>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

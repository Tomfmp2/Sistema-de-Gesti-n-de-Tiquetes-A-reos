using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.UseCases;

public interface IGetFlightByIdUseCase
{
    Task<Flight?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetFlightByIdUseCase : IGetFlightByIdUseCase
{
    private readonly IFlightRepository _repository;

    public GetFlightByIdUseCase(IFlightRepository repository)
    {
        _repository = repository;
    }

    public Task<Flight?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Flight?>(null);
        }

        return _repository.GetByIdAsync(FlightId.Create(id), cancellationToken);
    }
}

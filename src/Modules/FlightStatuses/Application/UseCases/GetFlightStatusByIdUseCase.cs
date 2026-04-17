using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;

public interface IGetFlightStatusByIdUseCase
{
    Task<FlightStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetFlightStatusByIdUseCase : IGetFlightStatusByIdUseCase
{
    private readonly IFlightStatusRepository _repository;

    public GetFlightStatusByIdUseCase(IFlightStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<FlightStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<FlightStatus?>(null);
        }

        return _repository.GetByIdAsync(FlightStatusId.Create(id), cancellationToken);
    }
}

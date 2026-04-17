using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.UseCases;

public sealed class GetAllFlightSeatsUseCase
{
    private readonly IFlightSeatRepository _repository;

    public GetAllFlightSeatsUseCase(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<FlightSeat>> ExecuteAsync(
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}

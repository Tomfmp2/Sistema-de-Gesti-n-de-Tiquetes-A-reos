using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.UseCases;

public sealed class GetFlightSeatByIdUseCase
{
    private readonly IFlightSeatRepository _repository;

    public GetFlightSeatByIdUseCase(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<FlightSeat?> ExecuteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(new FlightSeatId(id), cancellationToken);
    }
}

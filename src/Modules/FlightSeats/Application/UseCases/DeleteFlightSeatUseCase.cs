using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.UseCases;

public sealed class DeleteFlightSeatUseCase
{
    private readonly IFlightSeatRepository _repository;

    public DeleteFlightSeatUseCase(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> ExecuteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(new FlightSeatId(id), cancellationToken);
    }
}

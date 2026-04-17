using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Application.Interfaces;

public interface IFlightService
{
    Task<Flight> CreateAsync(
        CreateFlightRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Flight?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Flight>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateFlightRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.Repositories;

public interface IFlightRepository
{
    Task<Flight?> GetByIdAsync(FlightId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Flight>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Flight> AddAsync(Flight entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Flight entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(FlightId id, CancellationToken cancellationToken = default);
}

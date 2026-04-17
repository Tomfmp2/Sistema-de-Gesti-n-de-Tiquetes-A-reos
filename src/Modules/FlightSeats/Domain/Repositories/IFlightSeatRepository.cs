using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.Repositories;

public interface IFlightSeatRepository
{
    Task<FlightSeat?> GetByIdAsync(FlightSeatId id, CancellationToken cancellationToken = default);
    Task<IEnumerable<FlightSeat>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlightSeat> AddAsync(FlightSeat flightSeat, CancellationToken cancellationToken = default);
    Task<FlightSeat> UpdateAsync(FlightSeat flightSeat, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(FlightSeatId id, CancellationToken cancellationToken = default);
}

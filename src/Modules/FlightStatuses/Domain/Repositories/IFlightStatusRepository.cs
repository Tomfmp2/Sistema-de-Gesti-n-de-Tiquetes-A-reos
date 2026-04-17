using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;

public interface IFlightStatusRepository
{
    Task<FlightStatus?> GetByIdAsync(FlightStatusId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FlightStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlightStatus> AddAsync(FlightStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FlightStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(FlightStatusId id, CancellationToken cancellationToken = default);
}

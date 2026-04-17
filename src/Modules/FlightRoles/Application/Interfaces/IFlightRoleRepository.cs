using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;

public interface IFlightRoleRepository
{
    Task<FlightRole?> GetByIdAsync(FlightRoleId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FlightRole>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<FlightRole> AddAsync(FlightRole entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(FlightRole entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(FlightRoleId id, CancellationToken cancellationToken = default);
}

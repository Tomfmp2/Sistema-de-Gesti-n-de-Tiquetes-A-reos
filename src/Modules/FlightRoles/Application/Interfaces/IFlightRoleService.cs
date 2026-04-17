using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;

public interface IFlightRoleService
{
    Task<FlightRole> CreateAsync(CreateFlightRoleRequest request, CancellationToken cancellationToken = default);
    Task<FlightRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FlightRole>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(UpdateFlightRoleRequest request, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

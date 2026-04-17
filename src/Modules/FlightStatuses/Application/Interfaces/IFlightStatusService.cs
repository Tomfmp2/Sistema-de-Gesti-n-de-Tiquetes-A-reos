using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.Interfaces;

public interface IFlightStatusService
{
    Task<FlightStatus> CreateAsync(
        CreateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task<FlightStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FlightStatus>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateFlightStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

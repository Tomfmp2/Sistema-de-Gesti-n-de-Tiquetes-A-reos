using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Interfaces;

public interface IFlightStatusTransitionService
{
    Task<FlightStatusTransition> CreateAsync(
        CreateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<FlightStatusTransition?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<FlightStatusTransition>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateFlightStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

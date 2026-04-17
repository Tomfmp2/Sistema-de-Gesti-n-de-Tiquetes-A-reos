using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Interfaces;

public interface IPassengerService
{
    Task<Passenger> CreateAsync(
        CreatePassengerRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Passenger?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Passenger>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePassengerRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

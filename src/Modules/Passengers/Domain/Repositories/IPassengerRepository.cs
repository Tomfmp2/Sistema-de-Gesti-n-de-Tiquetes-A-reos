using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Repositories;

public interface IPassengerRepository
{
    Task<Passenger?> GetByIdAsync(PassengerId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Passenger>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Passenger> AddAsync(Passenger entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Passenger entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PassengerId id, CancellationToken cancellationToken = default);
}

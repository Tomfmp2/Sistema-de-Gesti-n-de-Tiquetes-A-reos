using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Repositories;

public interface IFareRepository
{
    Task<Fare?> GetByIdAsync(FareId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Fare>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Fare> AddAsync(Fare entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Fare entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(FareId id, CancellationToken cancellationToken = default);
}

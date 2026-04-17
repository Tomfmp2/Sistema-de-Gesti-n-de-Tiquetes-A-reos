using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;

public interface IPassengerTypeRepository
{
    Task<PassengerType?> GetByIdAsync(
        PassengerTypeId id,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<PassengerType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PassengerType> AddAsync(PassengerType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PassengerType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PassengerTypeId id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Repositories;

public interface IPersonPhoneRepository
{
    Task<PersonPhone?> GetByIdAsync(
        PersonPhoneId id,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<PersonPhone>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PersonPhone> AddAsync(PersonPhone entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PersonPhone entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PersonPhoneId id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Repositories;

public interface IPersonEmailRepository
{
    Task<PersonEmail?> GetByIdAsync(
        PersonEmailId id,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<PersonEmail>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PersonEmail> AddAsync(PersonEmail entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PersonEmail entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PersonEmailId id, CancellationToken cancellationToken = default);
}

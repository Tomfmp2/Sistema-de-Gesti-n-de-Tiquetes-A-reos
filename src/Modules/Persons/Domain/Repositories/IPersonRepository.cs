using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Repositories;

public interface IPersonRepository
{
    Task<Person?> GetByIdAsync(PersonId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Person> AddAsync(Person entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Person entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PersonId id, CancellationToken cancellationToken = default);
}

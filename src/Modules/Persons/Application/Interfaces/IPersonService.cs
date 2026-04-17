using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Application.Interfaces;

public interface IPersonService
{
    Task<Person> CreateAsync(
        CreatePersonRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Person?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Person>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePersonRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

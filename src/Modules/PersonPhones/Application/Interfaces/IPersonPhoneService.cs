using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonPhones.Application.Interfaces;

public interface IPersonPhoneService
{
    Task<PersonPhone> CreateAsync(
        CreatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PersonPhone?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PersonPhone>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePersonPhoneRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

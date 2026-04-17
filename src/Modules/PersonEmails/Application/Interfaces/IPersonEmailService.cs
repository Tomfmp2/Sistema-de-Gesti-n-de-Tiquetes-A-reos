using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PersonEmails.Application.Interfaces;

public interface IPersonEmailService
{
    Task<PersonEmail> CreateAsync(
        CreatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PersonEmail?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PersonEmail>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePersonEmailRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

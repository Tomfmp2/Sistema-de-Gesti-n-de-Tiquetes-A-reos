using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Application.Interfaces;

public interface IEmailDomainService
{
    Task<EmailDomain> CreateAsync(
        CreateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    );

    Task<EmailDomain?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<EmailDomain>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateEmailDomainRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

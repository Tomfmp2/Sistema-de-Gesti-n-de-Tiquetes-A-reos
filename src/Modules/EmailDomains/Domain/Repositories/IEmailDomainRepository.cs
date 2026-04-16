using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Repositories;

public interface IEmailDomainRepository
{
    Task<EmailDomain?> GetByIdAsync(
        EmailDomainId id,
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyList<EmailDomain>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<EmailDomain> AddAsync(EmailDomain entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(EmailDomain entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(EmailDomainId id, CancellationToken cancellationToken = default);
}

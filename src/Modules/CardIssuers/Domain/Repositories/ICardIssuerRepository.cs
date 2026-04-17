using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;

public interface ICardIssuerRepository
{
    Task<CardIssuer?> GetByIdAsync(CardIssuerId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CardIssuer>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CardIssuer> AddAsync(CardIssuer entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CardIssuer entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(CardIssuerId id, CancellationToken cancellationToken = default);
}

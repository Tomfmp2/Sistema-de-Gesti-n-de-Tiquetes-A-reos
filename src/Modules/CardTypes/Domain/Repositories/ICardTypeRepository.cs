using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;

public interface ICardTypeRepository
{
    Task<CardType?> GetByIdAsync(CardTypeId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CardType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<CardType> AddAsync(CardType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(CardType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(CardTypeId id, CancellationToken cancellationToken = default);
}

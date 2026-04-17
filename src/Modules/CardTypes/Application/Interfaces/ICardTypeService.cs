using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Interfaces;

public interface ICardTypeService
{
    Task<CardType> CreateAsync(
        CreateCardTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<CardType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CardType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateCardTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Interfaces;

public interface ICardIssuerService
{
    Task<CardIssuer> CreateAsync(
        CreateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    );

    Task<CardIssuer?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<CardIssuer>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

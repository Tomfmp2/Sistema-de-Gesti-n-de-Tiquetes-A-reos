using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Services;

public sealed class CardIssuerService : ICardIssuerService
{
    private readonly ICreateCardIssuerUseCase _create;
    private readonly IGetCardIssuerByIdUseCase _getById;
    private readonly IGetAllCardIssuersUseCase _getAll;
    private readonly IUpdateCardIssuerUseCase _update;
    private readonly IDeleteCardIssuerUseCase _delete;

    public CardIssuerService(
        ICreateCardIssuerUseCase create,
        IGetCardIssuerByIdUseCase getById,
        IGetAllCardIssuersUseCase getAll,
        IUpdateCardIssuerUseCase update,
        IDeleteCardIssuerUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<CardIssuer> CreateAsync(
        CreateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<CardIssuer?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<CardIssuer>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

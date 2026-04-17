using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Services;

public sealed class CardTypeService : ICardTypeService
{
    private readonly ICreateCardTypeUseCase _create;
    private readonly IGetCardTypeByIdUseCase _getById;
    private readonly IGetAllCardTypesUseCase _getAll;
    private readonly IUpdateCardTypeUseCase _update;
    private readonly IDeleteCardTypeUseCase _delete;

    public CardTypeService(
        ICreateCardTypeUseCase create,
        IGetCardTypeByIdUseCase getById,
        IGetAllCardTypesUseCase getAll,
        IUpdateCardTypeUseCase update,
        IDeleteCardTypeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<CardType> CreateAsync(
        CreateCardTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<CardType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<CardType>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateCardTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.UseCases;

public interface IGetAllCardTypesUseCase
{
    Task<IReadOnlyList<CardType>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllCardTypesUseCase : IGetAllCardTypesUseCase
{
    private readonly ICardTypeRepository _repository;

    public GetAllCardTypesUseCase(ICardTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<CardType>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

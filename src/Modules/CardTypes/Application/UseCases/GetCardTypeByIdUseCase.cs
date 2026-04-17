using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.UseCases;

public interface IGetCardTypeByIdUseCase
{
    Task<CardType?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetCardTypeByIdUseCase : IGetCardTypeByIdUseCase
{
    private readonly ICardTypeRepository _repository;

    public GetCardTypeByIdUseCase(ICardTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<CardType?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<CardType?>(null);
        }

        return _repository.GetByIdAsync(CardTypeId.Create(id), cancellationToken);
    }
}

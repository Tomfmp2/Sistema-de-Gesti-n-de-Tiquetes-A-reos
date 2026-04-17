using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.UseCases;

public interface ICreateCardTypeUseCase
{
    Task<CardType> ExecuteAsync(
        CreateCardTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateCardTypeUseCase : ICreateCardTypeUseCase
{
    private readonly ICardTypeRepository _repository;

    public CreateCardTypeUseCase(ICardTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<CardType> ExecuteAsync(
        CreateCardTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = CardType.Create(new CardTypeId(0), CardTypeName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

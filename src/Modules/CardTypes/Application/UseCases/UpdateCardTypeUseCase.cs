using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.UseCases;

public interface IUpdateCardTypeUseCase
{
    Task ExecuteAsync(
        UpdateCardTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateCardTypeUseCase : IUpdateCardTypeUseCase
{
    private readonly ICardTypeRepository _repository;

    public UpdateCardTypeUseCase(ICardTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateCardTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = CardType.Create(CardTypeId.Create(request.Id), CardTypeName.Create(name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

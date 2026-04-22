using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.UseCases;

public interface IUpdateCardIssuerUseCase
{
    Task ExecuteAsync(
        UpdateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateCardIssuerUseCase : IUpdateCardIssuerUseCase
{
    private readonly ICardIssuerRepository _repository;

    public UpdateCardIssuerUseCase(ICardIssuerRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = CardIssuer.Create(CardIssuerId.Create(request.Id), CardIssuerName.Create(name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

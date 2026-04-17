using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.UseCases;

public interface ICreateCardIssuerUseCase
{
    Task<CardIssuer> ExecuteAsync(
        CreateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateCardIssuerUseCase : ICreateCardIssuerUseCase
{
    private readonly ICardIssuerRepository _repository;

    public CreateCardIssuerUseCase(ICardIssuerRepository repository)
    {
        _repository = repository;
    }

    public Task<CardIssuer> ExecuteAsync(
        CreateCardIssuerRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = CardIssuer.Create(new CardIssuerId(0), CardIssuerName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

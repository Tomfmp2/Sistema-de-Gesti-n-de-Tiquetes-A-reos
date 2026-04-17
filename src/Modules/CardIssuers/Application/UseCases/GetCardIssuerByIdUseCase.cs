using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.UseCases;

public interface IGetCardIssuerByIdUseCase
{
    Task<CardIssuer?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetCardIssuerByIdUseCase : IGetCardIssuerByIdUseCase
{
    private readonly ICardIssuerRepository _repository;

    public GetCardIssuerByIdUseCase(ICardIssuerRepository repository)
    {
        _repository = repository;
    }

    public Task<CardIssuer?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<CardIssuer?>(null);
        }

        return _repository.GetByIdAsync(CardIssuerId.Create(id), cancellationToken);
    }
}

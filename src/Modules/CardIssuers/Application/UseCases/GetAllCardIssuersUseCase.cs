using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.UseCases;

public interface IGetAllCardIssuersUseCase
{
    Task<IReadOnlyList<CardIssuer>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllCardIssuersUseCase : IGetAllCardIssuersUseCase
{
    private readonly ICardIssuerRepository _repository;

    public GetAllCardIssuersUseCase(ICardIssuerRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<CardIssuer>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Application.UseCases;

public interface IDeleteCardIssuerUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteCardIssuerUseCase : IDeleteCardIssuerUseCase
{
    private readonly ICardIssuerRepository _repository;

    public DeleteCardIssuerUseCase(ICardIssuerRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(CardIssuerId.Create(id), cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Application.UseCases;

public interface IDeleteCardTypeUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteCardTypeUseCase : IDeleteCardTypeUseCase
{
    private readonly ICardTypeRepository _repository;

    public DeleteCardTypeUseCase(ICardTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(CardTypeId.Create(id), cancellationToken);
    }
}

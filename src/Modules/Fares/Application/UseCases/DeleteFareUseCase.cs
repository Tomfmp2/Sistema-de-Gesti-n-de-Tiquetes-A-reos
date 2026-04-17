using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;

public interface IDeleteFareUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteFareUseCase : IDeleteFareUseCase
{
    private readonly IFareRepository _repository;

    public DeleteFareUseCase(IFareRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(FareId.Create(id), cancellationToken);
    }
}

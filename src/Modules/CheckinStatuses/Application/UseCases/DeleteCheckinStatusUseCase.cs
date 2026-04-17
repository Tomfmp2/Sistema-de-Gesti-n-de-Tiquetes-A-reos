using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.UseCases;

public interface IDeleteCheckinStatusUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteCheckinStatusUseCase : IDeleteCheckinStatusUseCase
{
    private readonly ICheckinStatusRepository _repository;

    public DeleteCheckinStatusUseCase(ICheckinStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(CheckinStatusId.Create(id), cancellationToken);
    }
}

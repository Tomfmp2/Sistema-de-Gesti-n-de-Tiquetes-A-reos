using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;

public interface IDeleteCheckinUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteCheckinUseCase : IDeleteCheckinUseCase
{
    private readonly ICheckinRepository _repository;

    public DeleteCheckinUseCase(ICheckinRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(CheckinId.Create(id), cancellationToken);
    }
}

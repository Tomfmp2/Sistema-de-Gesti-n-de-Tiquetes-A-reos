using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.UseCases;

public interface IDeleteSessionUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteSessionUseCase : IDeleteSessionUseCase
{
    private readonly ISessionRepository _repository;

    public DeleteSessionUseCase(ISessionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(SessionId.Create(id), cancellationToken);
    }
}

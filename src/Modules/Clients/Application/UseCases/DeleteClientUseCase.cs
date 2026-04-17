using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.UseCases;

public interface IDeleteClientUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteClientUseCase : IDeleteClientUseCase
{
    private readonly IClientRepository _repository;

    public DeleteClientUseCase(IClientRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(ClientId.Create(id), cancellationToken);
    }
}

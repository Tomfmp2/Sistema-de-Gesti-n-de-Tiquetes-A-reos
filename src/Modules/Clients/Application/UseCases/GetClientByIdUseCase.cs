using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.UseCases;

public interface IGetClientByIdUseCase
{
    Task<Client?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetClientByIdUseCase : IGetClientByIdUseCase
{
    private readonly IClientRepository _repository;

    public GetClientByIdUseCase(IClientRepository repository)
    {
        _repository = repository;
    }

    public Task<Client?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Client?>(null);
        }

        return _repository.GetByIdAsync(ClientId.Create(id), cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.UseCases;

public interface IGetAllClientsUseCase
{
    Task<IReadOnlyList<Client>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllClientsUseCase : IGetAllClientsUseCase
{
    private readonly IClientRepository _repository;

    public GetAllClientsUseCase(IClientRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Client>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Services;

public sealed class ClientService : IClientService
{
    private readonly ICreateClientUseCase _create;
    private readonly IGetClientByIdUseCase _getById;
    private readonly IGetAllClientsUseCase _getAll;
    private readonly IUpdateClientUseCase _update;
    private readonly IDeleteClientUseCase _delete;

    public ClientService(
        ICreateClientUseCase create,
        IGetClientByIdUseCase getById,
        IGetAllClientsUseCase getAll,
        IUpdateClientUseCase update,
        IDeleteClientUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Client> CreateAsync(
        CreateClientRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateClientRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.UseCases;

public interface IUpdateClientUseCase
{
    Task ExecuteAsync(
        UpdateClientRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateClientUseCase : IUpdateClientUseCase
{
    private readonly IClientRepository _repository;

    public UpdateClientUseCase(IClientRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(
        UpdateClientRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var existing = await _repository.GetByIdAsync(ClientId.Create(request.Id), cancellationToken);
        if (existing is null)
        {
            throw new InvalidOperationException($"No existe cliente {request.Id}.");
        }

        var x = Client.Create(
            ClientId.Create(request.Id),
            ClientPersonId.Create(request.PersonId),
            existing.CreatedAt
        );
        await _repository.UpdateAsync(x, cancellationToken);
    }
}

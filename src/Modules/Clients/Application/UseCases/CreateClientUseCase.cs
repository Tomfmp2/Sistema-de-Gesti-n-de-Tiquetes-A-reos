using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.UseCases;

public interface ICreateClientUseCase
{
    Task<Client> ExecuteAsync(
        CreateClientRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateClientUseCase : ICreateClientUseCase
{
    private readonly IClientRepository _repository;

    public CreateClientUseCase(IClientRepository repository)
    {
        _repository = repository;
    }

    public Task<Client> ExecuteAsync(
        CreateClientRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Client.CreateNew(ClientPersonId.Create(request.PersonId));
        return _repository.AddAsync(x, cancellationToken);
    }
}

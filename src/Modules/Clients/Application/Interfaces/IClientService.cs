using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Application.Interfaces;

public interface IClientService
{
    Task<Client> CreateAsync(
        CreateClientRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Client?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateClientRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

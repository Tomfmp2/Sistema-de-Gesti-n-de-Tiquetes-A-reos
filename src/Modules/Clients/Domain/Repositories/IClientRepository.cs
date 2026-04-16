using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(ClientId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Client>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Client> AddAsync(Client entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Client entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ClientId id, CancellationToken cancellationToken = default);
}

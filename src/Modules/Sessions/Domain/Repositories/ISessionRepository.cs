using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;

public interface ISessionRepository
{
    Task<Session?> GetByIdAsync(SessionId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Session> AddAsync(Session entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Session entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(SessionId id, CancellationToken cancellationToken = default);
}

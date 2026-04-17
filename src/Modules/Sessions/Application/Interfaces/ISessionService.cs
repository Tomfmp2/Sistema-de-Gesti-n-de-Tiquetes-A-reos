using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Interfaces;

public interface ISessionService
{
    Task<Session> CreateAsync(
        CreateSessionRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Session?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Session>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateSessionRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

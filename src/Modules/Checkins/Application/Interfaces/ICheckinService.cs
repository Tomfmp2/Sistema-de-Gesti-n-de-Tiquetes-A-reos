using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Interfaces;

public interface ICheckinService
{
    Task<Checkin> CreateAsync(
        CreateCheckinRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Checkin?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Checkin>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateCheckinRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Interfaces;

public interface IFareService
{
    Task<Fare> CreateAsync(
        CreateFareRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Fare?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Fare>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateFareRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

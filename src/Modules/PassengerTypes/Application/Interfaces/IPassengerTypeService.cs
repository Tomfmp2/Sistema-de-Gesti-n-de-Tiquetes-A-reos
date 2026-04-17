using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Interfaces;

public interface IPassengerTypeService
{
    Task<PassengerType> CreateAsync(
        CreatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PassengerType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PassengerType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

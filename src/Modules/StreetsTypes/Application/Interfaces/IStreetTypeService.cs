using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Interfaces;

public interface IStreetTypeService
{
    Task<StreetType> CreateAsync(
        CreateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<StreetType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<StreetType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

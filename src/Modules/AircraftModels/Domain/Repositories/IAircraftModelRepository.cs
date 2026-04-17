using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;

public interface IAircraftModelRepository
{
    Task<AircraftModel?> GetByIdAsync(AircraftModelId id);
    Task<IEnumerable<AircraftModel>> GetAllAsync();
    Task AddAsync(AircraftModel aircraftModel);
    Task UpdateAsync(AircraftModel aircraftModel);
    Task DeleteAsync(AircraftModelId id);
}
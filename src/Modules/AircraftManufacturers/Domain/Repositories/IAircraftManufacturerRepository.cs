using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;

public interface IAircraftManufacturerRepository
{
    Task<AircraftManufacturer?> GetByIdAsync(AircraftManufacturerId id);
    Task<IEnumerable<AircraftManufacturer>> GetAllAsync();
    Task AddAsync(AircraftManufacturer aircraftManufacturer);
    Task UpdateAsync(AircraftManufacturer aircraftManufacturer);
    Task DeleteAsync(AircraftManufacturerId id);
}
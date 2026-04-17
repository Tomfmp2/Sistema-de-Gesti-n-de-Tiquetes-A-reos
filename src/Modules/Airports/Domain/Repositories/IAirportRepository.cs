using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Repositories;

public interface IAirportRepository
{
    Task<Airport?> GetByIdAsync(AirportId id);
    Task<IEnumerable<Airport>> GetAllAsync();
    Task AddAsync(Airport airport);
    Task UpdateAsync(Airport airport);
    Task DeleteAsync(AirportId id);
}
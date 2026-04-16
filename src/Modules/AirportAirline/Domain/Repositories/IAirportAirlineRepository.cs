using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Domain.Repositories;

public interface IAirportAirlineRepository
{
    Task<AirportAirlineRecord?> GetByIdAsync(AirportAirlineId id);
    Task<IEnumerable<AirportAirlineRecord>> GetAllAsync();
    Task AddAsync(AirportAirlineRecord airportAirline);
    Task UpdateAsync(AirportAirlineRecord airportAirline);
    Task DeleteAsync(AirportAirlineId id);
}
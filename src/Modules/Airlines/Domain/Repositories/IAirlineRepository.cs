using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;

public interface IAirlineRepository
{
    Task<Airline?> GetByIdAsync(AirlineId id);
    Task<IEnumerable<Airline>> GetAllAsync();
    Task AddAsync(Airline airline);
    Task UpdateAsync(Airline airline);
    Task DeleteAsync(AirlineId id);
}
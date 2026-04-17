using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.Repositories;

public interface IFlightStatusRepository
{
    Task<FlightStatus> GetByIdAsync(FlightStatusId id);
    Task<IEnumerable<FlightStatus>> GetAllAsync();
    Task AddAsync(FlightStatus flightStatus);
    Task UpdateAsync(FlightStatus flightStatus);
    Task DeleteAsync(FlightStatusId id);
}
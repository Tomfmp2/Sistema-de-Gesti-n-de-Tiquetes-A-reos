using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Repositories;

public interface IRouteRepository
{
    Task<Route?> GetByIdAsync(RouteId id);
    Task<IEnumerable<Route>> GetAllAsync();
    Task AddAsync(Route route);
    Task UpdateAsync(Route route);
    Task DeleteAsync(RouteId id);
}
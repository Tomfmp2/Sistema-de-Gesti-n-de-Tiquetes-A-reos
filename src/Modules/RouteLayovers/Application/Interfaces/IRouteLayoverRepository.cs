using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.Interfaces;

public interface IRouteLayoverRepository
{
    Task<RouteLayover?> GetByIdAsync(RouteLayoverId id);
    Task<IEnumerable<RouteLayover>> GetAllAsync();
    Task AddAsync(RouteLayover routeLayover);
    Task UpdateAsync(RouteLayover routeLayover);
    Task DeleteAsync(RouteLayoverId id);
}
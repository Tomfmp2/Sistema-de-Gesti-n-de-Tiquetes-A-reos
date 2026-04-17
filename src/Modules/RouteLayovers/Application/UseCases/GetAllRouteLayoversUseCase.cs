using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;

public class GetAllRouteLayoversUseCase
{
    private readonly IRouteLayoverRepository _repository;

    public GetAllRouteLayoversUseCase(IRouteLayoverRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<RouteLayover>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
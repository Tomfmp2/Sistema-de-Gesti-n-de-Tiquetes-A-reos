using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;

public class GetRouteLayoverByIdUseCase
{
    private readonly IRouteLayoverRepository _repository;

    public GetRouteLayoverByIdUseCase(IRouteLayoverRepository repository)
    {
        _repository = repository;
    }

    public async Task<RouteLayover> ExecuteAsync(RouteLayoverId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
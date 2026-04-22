using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.UseCases;

public class GetRouteByIdUseCase
{
    private readonly IRouteRepository _repository;

    public GetRouteByIdUseCase(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<Route?> ExecuteAsync(RouteId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
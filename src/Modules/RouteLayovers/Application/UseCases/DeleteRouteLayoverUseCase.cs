using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;

public class DeleteRouteLayoverUseCase
{
    private readonly IRouteLayoverRepository _repository;

    public DeleteRouteLayoverUseCase(IRouteLayoverRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(RouteLayoverId id)
    {
        await _repository.DeleteAsync(id);
    }
}
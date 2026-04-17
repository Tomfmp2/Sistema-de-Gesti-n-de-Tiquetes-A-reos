using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.UseCases;

public class GetAllRoutesUseCase
{
    private readonly IRouteRepository _repository;

    public GetAllRoutesUseCase(IRouteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Route>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
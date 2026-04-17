using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;

public class GetAllAirlinesUseCase
{
    private readonly IAirlineRepository _repository;

    public GetAllAirlinesUseCase(IAirlineRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Airline>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;

public class GetAllSeasonsUseCase
{
    private readonly ISeasonRepository _repository;

    public GetAllSeasonsUseCase(ISeasonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Season>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
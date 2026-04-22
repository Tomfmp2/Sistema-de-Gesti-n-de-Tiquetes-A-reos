using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;

public class GetSeasonByIdUseCase
{
    private readonly ISeasonRepository _repository;

    public GetSeasonByIdUseCase(ISeasonRepository repository)
    {
        _repository = repository;
    }

    public async Task<Season?> ExecuteAsync(SeasonId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
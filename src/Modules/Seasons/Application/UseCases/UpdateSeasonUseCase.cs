using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;

public class UpdateSeasonUseCase
{
    private readonly ISeasonRepository _repository;

    public UpdateSeasonUseCase(ISeasonRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SeasonId id, SeasonName name, SeasonDescription description, PriceFactor priceFactor)
    {
        var season = Season.Reconstitute(id, name, description, priceFactor);
        await _repository.UpdateAsync(season);
    }
}
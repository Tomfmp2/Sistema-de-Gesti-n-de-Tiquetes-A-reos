using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;

public class CreateSeasonUseCase
{
    private readonly ISeasonRepository _repository;

    public CreateSeasonUseCase(ISeasonRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SeasonName name, SeasonDescription description, PriceFactor priceFactor)
    {
        var season = Season.Create(name, description, priceFactor);
        await _repository.AddAsync(season);
    }
}
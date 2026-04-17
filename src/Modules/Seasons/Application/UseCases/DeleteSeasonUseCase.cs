using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;

public class DeleteSeasonUseCase
{
    private readonly ISeasonRepository _repository;

    public DeleteSeasonUseCase(ISeasonRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SeasonId id)
    {
        await _repository.DeleteAsync(id);
    }
}
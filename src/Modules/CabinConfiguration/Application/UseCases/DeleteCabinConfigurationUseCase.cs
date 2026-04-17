using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;

public class DeleteCabinConfigurationUseCase
{
    private readonly ICabinConfigurationRepository _repository;

    public DeleteCabinConfigurationUseCase(ICabinConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(CabinConfigurationId id)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
        {
            throw new InvalidOperationException("Cabin configuration not found.");
        }

        await _repository.DeleteAsync(id);
    }
}
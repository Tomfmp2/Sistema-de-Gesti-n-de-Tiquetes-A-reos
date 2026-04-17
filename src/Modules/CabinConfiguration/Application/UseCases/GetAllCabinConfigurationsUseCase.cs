using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;

public class GetAllCabinConfigurationsUseCase
{
    private readonly ICabinConfigurationRepository _repository;

    public GetAllCabinConfigurationsUseCase(ICabinConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Aggregate.CabinConfiguration>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
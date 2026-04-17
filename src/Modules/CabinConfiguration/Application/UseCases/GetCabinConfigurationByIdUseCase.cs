using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;

public class GetCabinConfigurationByIdUseCase
{
    private readonly ICabinConfigurationRepository _repository;

    public GetCabinConfigurationByIdUseCase(ICabinConfigurationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Aggregate.CabinConfiguration?> ExecuteAsync(CabinConfigurationId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
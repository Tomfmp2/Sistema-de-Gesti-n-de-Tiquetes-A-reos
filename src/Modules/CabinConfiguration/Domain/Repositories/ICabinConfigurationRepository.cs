using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;

public interface ICabinConfigurationRepository
{
    Task<Aggregate.CabinConfiguration?> GetByIdAsync(CabinConfigurationId id);
    Task<IEnumerable<Aggregate.CabinConfiguration>> GetAllAsync();
    Task AddAsync(Aggregate.CabinConfiguration cabinConfiguration);
    Task UpdateAsync(Aggregate.CabinConfiguration cabinConfiguration);
    Task DeleteAsync(CabinConfigurationId id);
}
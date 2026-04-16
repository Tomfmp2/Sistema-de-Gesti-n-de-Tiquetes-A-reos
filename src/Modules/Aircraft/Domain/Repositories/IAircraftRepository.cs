using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Repositories;

public interface IAircraftRepository
{
    Task<Aggregate.Aircraft?> GetByIdAsync(AircraftId id);
    Task<IEnumerable<Aggregate.Aircraft>> GetAllAsync();
    Task AddAsync(Aggregate.Aircraft aircraft);
    Task UpdateAsync(Aggregate.Aircraft aircraft);
    Task DeleteAsync(AircraftId id);
}
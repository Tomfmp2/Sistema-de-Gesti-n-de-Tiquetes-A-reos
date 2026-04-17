using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Repositories;

public interface ICabinTypeRepository
{
    Task<Aggregate.CabinType?> GetByIdAsync(CabinTypeId id);
    Task<IEnumerable<Aggregate.CabinType>> GetAllAsync();
    Task AddAsync(Aggregate.CabinType cabinType);
    Task UpdateAsync(Aggregate.CabinType cabinType);
    Task DeleteAsync(CabinTypeId id);
}
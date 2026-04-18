namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.Repositories;

using Aggregate;

public interface IBaggageTypeRepository
{
    Task<BaggageType> CreateAsync(BaggageType baggageType);
    Task<BaggageType?> GetByIdAsync(int id);
    Task<List<BaggageType>> GetAllAsync();
    Task<BaggageType> UpdateAsync(BaggageType baggageType);
    Task DeleteAsync(int id);
}

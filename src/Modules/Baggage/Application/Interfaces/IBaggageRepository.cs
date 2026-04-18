using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.Interfaces;

public interface IBaggageRepository
{
    Task CreateAsync(BaggageItem baggage);
    Task<BaggageItem?> GetByIdAsync(int id);
    Task<List<BaggageItem>> GetAllAsync();
    Task UpdateAsync(BaggageItem baggage);
    Task DeleteAsync(int id);
}

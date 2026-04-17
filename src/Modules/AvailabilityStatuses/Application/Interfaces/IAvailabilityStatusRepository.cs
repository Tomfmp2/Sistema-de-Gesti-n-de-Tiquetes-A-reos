using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Application.Interfaces;

public interface IAvailabilityStatusRepository
{
    Task<AvailabilityStatus?> GetByIdAsync(AvailabilityStatusId id);
    Task<IEnumerable<AvailabilityStatus>> GetAllAsync();
    Task AddAsync(AvailabilityStatus availabilityStatus);
    Task UpdateAsync(AvailabilityStatus availabilityStatus);
    Task DeleteAsync(AvailabilityStatusId id);
}
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffAvailability.Domain.Repositories;

public interface IStaffAvailabilityRepository
{
    Task<StaffAvailabilityRecord?> GetByIdAsync(StaffAvailabilityId id);
    Task<IEnumerable<StaffAvailabilityRecord>> GetAllAsync();
    Task AddAsync(StaffAvailabilityRecord staffAvailability);
    Task UpdateAsync(StaffAvailabilityRecord staffAvailability);
    Task DeleteAsync(StaffAvailabilityId id);
}
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Repositories;

public interface IStaffRepository
{
    Task<StaffRecord?> GetByIdAsync(StaffId id);
    Task<IEnumerable<StaffRecord>> GetAllAsync();
    Task AddAsync(StaffRecord staff);
    Task UpdateAsync(StaffRecord staff);
    Task DeleteAsync(StaffId id);
}
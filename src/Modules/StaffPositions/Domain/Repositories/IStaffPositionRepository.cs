using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Repositories;

public interface IStaffPositionRepository
{
    Task<StaffPosition?> GetByIdAsync(StaffPositionId id);
    Task<IEnumerable<StaffPosition>> GetAllAsync();
    Task AddAsync(StaffPosition staffPosition);
    Task UpdateAsync(StaffPosition staffPosition);
    Task DeleteAsync(StaffPositionId id);
}
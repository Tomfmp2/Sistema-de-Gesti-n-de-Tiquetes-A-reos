using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.Interfaces;

public interface ISeatLocationTypeRepository
{
    Task<SeatLocationType> GetByIdAsync(SeatLocationTypeId id);
    Task<IEnumerable<SeatLocationType>> GetAllAsync();
    Task AddAsync(SeatLocationType seatLocationType);
    Task UpdateAsync(SeatLocationType seatLocationType);
    Task DeleteAsync(SeatLocationTypeId id);
}
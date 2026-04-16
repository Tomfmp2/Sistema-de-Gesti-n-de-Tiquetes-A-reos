using System.Collections.Generic;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Repositories;

public interface ISeasonRepository
{
    Task<Season> GetByIdAsync(SeasonId id);
    Task<IEnumerable<Season>> GetAllAsync();
    Task AddAsync(Season season);
    Task UpdateAsync(Season season);
    Task DeleteAsync(SeasonId id);
}
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Repositories;

public interface IPhoneCodeRepository
{
    Task<PhoneCode?> GetByIdAsync(PhoneCodeId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PhoneCode>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PhoneCode> AddAsync(PhoneCode entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PhoneCode entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PhoneCodeId id, CancellationToken cancellationToken = default);
}

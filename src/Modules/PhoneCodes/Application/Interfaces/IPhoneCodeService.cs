using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Interfaces;

public interface IPhoneCodeService
{
    Task<PhoneCode> CreateAsync(
        CreatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PhoneCode?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PhoneCode>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePhoneCodeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

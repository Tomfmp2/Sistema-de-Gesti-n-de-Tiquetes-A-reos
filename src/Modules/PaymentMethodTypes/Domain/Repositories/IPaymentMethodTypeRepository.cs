using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;

public interface IPaymentMethodTypeRepository
{
    Task<PaymentMethodType?> GetByIdAsync(PaymentMethodTypeId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PaymentMethodType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentMethodType> AddAsync(PaymentMethodType entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PaymentMethodType entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PaymentMethodTypeId id, CancellationToken cancellationToken = default);
}

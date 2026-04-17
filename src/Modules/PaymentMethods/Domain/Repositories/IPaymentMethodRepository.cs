using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;

public interface IPaymentMethodRepository
{
    Task<PaymentMethod?> GetByIdAsync(PaymentMethodId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentMethod> AddAsync(PaymentMethod entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PaymentMethod entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PaymentMethodId id, CancellationToken cancellationToken = default);
}

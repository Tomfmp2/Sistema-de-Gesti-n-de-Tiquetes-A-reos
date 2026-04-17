using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Repositories;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(PaymentId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Payment> AddAsync(Payment entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Payment entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PaymentId id, CancellationToken cancellationToken = default);
}

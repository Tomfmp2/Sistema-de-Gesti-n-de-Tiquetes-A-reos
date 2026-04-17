using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;

public interface IPaymentStatusRepository
{
    Task<PaymentStatus?> GetByIdAsync(PaymentStatusId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<PaymentStatus> AddAsync(PaymentStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(PaymentStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(PaymentStatusId id, CancellationToken cancellationToken = default);
}

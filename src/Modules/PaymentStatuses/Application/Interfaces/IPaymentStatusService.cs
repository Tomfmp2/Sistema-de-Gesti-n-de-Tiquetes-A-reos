using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Interfaces;

public interface IPaymentStatusService
{
    Task<PaymentStatus> CreateAsync(
        CreatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PaymentStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

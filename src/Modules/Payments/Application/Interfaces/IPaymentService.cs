using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Interfaces;

public interface IPaymentService
{
    Task<Payment> CreateAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePaymentRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

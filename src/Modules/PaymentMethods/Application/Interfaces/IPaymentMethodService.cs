using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Interfaces;

public interface IPaymentMethodService
{
    Task<PaymentMethod> CreateAsync(
        CreatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

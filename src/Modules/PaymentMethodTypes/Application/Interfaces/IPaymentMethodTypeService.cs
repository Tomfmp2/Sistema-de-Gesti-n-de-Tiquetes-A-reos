using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Interfaces;

public interface IPaymentMethodTypeService
{
    Task<PaymentMethodType> CreateAsync(
        CreatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task<PaymentMethodType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PaymentMethodType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Services;

public sealed class PaymentMethodService : IPaymentMethodService
{
    private readonly ICreatePaymentMethodUseCase _create;
    private readonly IGetPaymentMethodByIdUseCase _getById;
    private readonly IGetAllPaymentMethodsUseCase _getAll;
    private readonly IUpdatePaymentMethodUseCase _update;
    private readonly IDeletePaymentMethodUseCase _delete;

    public PaymentMethodService(
        ICreatePaymentMethodUseCase create,
        IGetPaymentMethodByIdUseCase getById,
        IGetAllPaymentMethodsUseCase getAll,
        IUpdatePaymentMethodUseCase update,
        IDeletePaymentMethodUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PaymentMethod> CreateAsync(
        CreatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PaymentMethod?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PaymentMethod>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

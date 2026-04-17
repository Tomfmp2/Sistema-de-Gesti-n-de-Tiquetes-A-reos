using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Services;

public sealed class PaymentMethodTypeService : IPaymentMethodTypeService
{
    private readonly ICreatePaymentMethodTypeUseCase _create;
    private readonly IGetPaymentMethodTypeByIdUseCase _getById;
    private readonly IGetAllPaymentMethodTypesUseCase _getAll;
    private readonly IUpdatePaymentMethodTypeUseCase _update;
    private readonly IDeletePaymentMethodTypeUseCase _delete;

    public PaymentMethodTypeService(
        ICreatePaymentMethodTypeUseCase create,
        IGetPaymentMethodTypeByIdUseCase getById,
        IGetAllPaymentMethodTypesUseCase getAll,
        IUpdatePaymentMethodTypeUseCase update,
        IDeletePaymentMethodTypeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PaymentMethodType> CreateAsync(
        CreatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PaymentMethodType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PaymentMethodType>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

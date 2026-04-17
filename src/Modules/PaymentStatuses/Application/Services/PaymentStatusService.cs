using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.Services;

public sealed class PaymentStatusService : IPaymentStatusService
{
    private readonly ICreatePaymentStatusUseCase _create;
    private readonly IGetPaymentStatusByIdUseCase _getById;
    private readonly IGetAllPaymentStatusesUseCase _getAll;
    private readonly IUpdatePaymentStatusUseCase _update;
    private readonly IDeletePaymentStatusUseCase _delete;

    public PaymentStatusService(
        ICreatePaymentStatusUseCase create,
        IGetPaymentStatusByIdUseCase getById,
        IGetAllPaymentStatusesUseCase getAll,
        IUpdatePaymentStatusUseCase update,
        IDeletePaymentStatusUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<PaymentStatus> CreateAsync(
        CreatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<PaymentStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<PaymentStatus>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePaymentStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

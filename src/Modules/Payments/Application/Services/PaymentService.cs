using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.Services;

public sealed class PaymentService : IPaymentService
{
    private readonly ICreatePaymentUseCase _create;
    private readonly IGetPaymentByIdUseCase _getById;
    private readonly IGetAllPaymentsUseCase _getAll;
    private readonly IUpdatePaymentUseCase _update;
    private readonly IDeletePaymentUseCase _delete;

    public PaymentService(
        ICreatePaymentUseCase create,
        IGetPaymentByIdUseCase getById,
        IGetAllPaymentsUseCase getAll,
        IUpdatePaymentUseCase update,
        IDeletePaymentUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Payment> CreateAsync(
        CreatePaymentRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Payment?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Payment>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePaymentRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

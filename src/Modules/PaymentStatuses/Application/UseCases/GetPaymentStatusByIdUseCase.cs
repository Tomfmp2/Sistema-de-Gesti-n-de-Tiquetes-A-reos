using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Application.UseCases;

public interface IGetPaymentStatusByIdUseCase
{
    Task<PaymentStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPaymentStatusByIdUseCase : IGetPaymentStatusByIdUseCase
{
    private readonly IPaymentStatusRepository _repository;

    public GetPaymentStatusByIdUseCase(IPaymentStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<PaymentStatus?>(null);
        }

        return _repository.GetByIdAsync(PaymentStatusId.Create(id), cancellationToken);
    }
}

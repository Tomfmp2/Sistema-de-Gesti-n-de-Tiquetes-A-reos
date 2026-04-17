using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Payments.Application.UseCases;

public interface IGetPaymentByIdUseCase
{
    Task<Payment?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetPaymentByIdUseCase : IGetPaymentByIdUseCase
{
    private readonly IPaymentRepository _repository;

    public GetPaymentByIdUseCase(IPaymentRepository repository)
    {
        _repository = repository;
    }

    public Task<Payment?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Payment?>(null);
        }

        return _repository.GetByIdAsync(PaymentId.Create(id), cancellationToken);
    }
}

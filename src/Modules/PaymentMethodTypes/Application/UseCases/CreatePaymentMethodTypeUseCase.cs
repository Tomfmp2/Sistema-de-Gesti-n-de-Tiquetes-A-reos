using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.UseCases;

public interface ICreatePaymentMethodTypeUseCase
{
    Task<PaymentMethodType> ExecuteAsync(
        CreatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePaymentMethodTypeUseCase : ICreatePaymentMethodTypeUseCase
{
    private readonly IPaymentMethodTypeRepository _repository;

    public CreatePaymentMethodTypeUseCase(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentMethodType> ExecuteAsync(
        CreatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PaymentMethodType.Create(new PaymentMethodTypeId(0), PaymentMethodTypeName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

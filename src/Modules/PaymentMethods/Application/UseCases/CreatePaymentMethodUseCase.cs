using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.UseCases;

public interface ICreatePaymentMethodUseCase
{
    Task<PaymentMethod> ExecuteAsync(
        CreatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePaymentMethodUseCase : ICreatePaymentMethodUseCase
{
    private readonly IPaymentMethodRepository _repository;

    public CreatePaymentMethodUseCase(IPaymentMethodRepository repository)
    {
        _repository = repository;
    }

    public Task<PaymentMethod> ExecuteAsync(
        CreatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var commercialName = request.CommercialName;
        ArgumentNullException.ThrowIfNull(commercialName);
        var x = PaymentMethod.Create(new PaymentMethodId(0), PaymentMethodPaymentMethodTypeId.Create(request.PaymentMethodTypeId), PaymentMethodCardTypeId.Create(request.CardTypeId), PaymentMethodCardIssuerId.Create(request.CardIssuerId), PaymentMethodCommercialName.Create(commercialName));
        return _repository.AddAsync(x, cancellationToken);
    }
}

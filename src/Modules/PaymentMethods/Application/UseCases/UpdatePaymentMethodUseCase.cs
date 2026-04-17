using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Application.UseCases;

public interface IUpdatePaymentMethodUseCase
{
    Task ExecuteAsync(
        UpdatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePaymentMethodUseCase : IUpdatePaymentMethodUseCase
{
    private readonly IPaymentMethodRepository _repository;

    public UpdatePaymentMethodUseCase(IPaymentMethodRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePaymentMethodRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PaymentMethod.Create(PaymentMethodId.Create(request.Id), PaymentMethodPaymentMethodTypeId.Create(request.PaymentMethodTypeId), PaymentMethodCardTypeId.Create(request.CardTypeId), PaymentMethodCardIssuerId.Create(request.CardIssuerId), PaymentMethodCommercialName.Create(request.CommercialName));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

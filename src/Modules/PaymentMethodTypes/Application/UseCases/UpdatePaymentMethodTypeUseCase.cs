using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Application.UseCases;

public interface IUpdatePaymentMethodTypeUseCase
{
    Task ExecuteAsync(
        UpdatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePaymentMethodTypeUseCase : IUpdatePaymentMethodTypeUseCase
{
    private readonly IPaymentMethodTypeRepository _repository;

    public UpdatePaymentMethodTypeUseCase(IPaymentMethodTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePaymentMethodTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = PaymentMethodType.Create(PaymentMethodTypeId.Create(request.Id), PaymentMethodTypeName.Create(name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

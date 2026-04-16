using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.UseCases;

public interface IUpdatePassengerTypeUseCase
{
    Task ExecuteAsync(
        UpdatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdatePassengerTypeUseCase : IUpdatePassengerTypeUseCase
{
    private readonly IPassengerTypeRepository _repository;

    public UpdatePassengerTypeUseCase(IPassengerTypeRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PassengerType.Create(
            PassengerTypeId.Create(request.Id),
            PassengerTypeName.Create(request.Name),
            request.MinAge,
            request.MaxAge
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

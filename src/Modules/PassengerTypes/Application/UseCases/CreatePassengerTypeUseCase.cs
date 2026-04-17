using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.UseCases;

public interface ICreatePassengerTypeUseCase
{
    Task<PassengerType> ExecuteAsync(
        CreatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreatePassengerTypeUseCase : ICreatePassengerTypeUseCase
{
    private readonly IPassengerTypeRepository _repository;

    public CreatePassengerTypeUseCase(IPassengerTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<PassengerType> ExecuteAsync(
        CreatePassengerTypeRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = PassengerType.CreateNew(
            PassengerTypeName.Create(request.Name),
            request.MinAge,
            request.MaxAge
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}

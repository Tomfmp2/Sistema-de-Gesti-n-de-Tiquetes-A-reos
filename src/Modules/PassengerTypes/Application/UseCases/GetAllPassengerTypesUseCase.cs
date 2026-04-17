using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.UseCases;

public interface IGetAllPassengerTypesUseCase
{
    Task<IReadOnlyList<PassengerType>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllPassengerTypesUseCase : IGetAllPassengerTypesUseCase
{
    private readonly IPassengerTypeRepository _repository;

    public GetAllPassengerTypesUseCase(IPassengerTypeRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<PassengerType>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

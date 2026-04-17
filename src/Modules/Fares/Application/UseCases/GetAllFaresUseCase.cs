using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;

public interface IGetAllFaresUseCase
{
    Task<IReadOnlyList<Fare>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllFaresUseCase : IGetAllFaresUseCase
{
    private readonly IFareRepository _repository;

    public GetAllFaresUseCase(IFareRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Fare>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

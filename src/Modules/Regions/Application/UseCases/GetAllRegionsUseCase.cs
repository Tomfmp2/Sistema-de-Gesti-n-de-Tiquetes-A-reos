using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.UseCases;

public interface IGetAllRegionsUseCase
{
    Task<IReadOnlyList<Region>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllRegionsUseCase : IGetAllRegionsUseCase
{
    private readonly IRegionRepository _repository;

    public GetAllRegionsUseCase(IRegionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Region>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

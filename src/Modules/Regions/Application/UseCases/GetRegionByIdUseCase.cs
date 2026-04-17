using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.UseCases;

public interface IGetRegionByIdUseCase
{
    Task<Region?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetRegionByIdUseCase : IGetRegionByIdUseCase
{
    private readonly IRegionRepository _repository;

    public GetRegionByIdUseCase(IRegionRepository repository)
    {
        _repository = repository;
    }

    public Task<Region?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Region?>(null);
        }

        return _repository.GetByIdAsync(RegionId.Create(id), cancellationToken);
    }
}

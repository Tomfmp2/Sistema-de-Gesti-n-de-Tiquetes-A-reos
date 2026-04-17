using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.UseCases;

public interface ICreateRegionUseCase
{
    Task<Region> ExecuteAsync(
        CreateRegionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateRegionUseCase : ICreateRegionUseCase
{
    private readonly IRegionRepository _repository;

    public CreateRegionUseCase(IRegionRepository repository)
    {
        _repository = repository;
    }

    public Task<Region> ExecuteAsync(
        CreateRegionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Region.CreateNew(
            RegionName.Create(request.Name),
            RegionType.Create(request.Type),
            RegionCuntryId.Create(request.CountryId)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}

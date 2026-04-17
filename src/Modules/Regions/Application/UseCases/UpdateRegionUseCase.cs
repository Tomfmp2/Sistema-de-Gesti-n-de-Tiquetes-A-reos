using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.UseCases;

public interface IUpdateRegionUseCase
{
    Task ExecuteAsync(
        UpdateRegionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateRegionUseCase : IUpdateRegionUseCase
{
    private readonly IRegionRepository _repository;

    public UpdateRegionUseCase(IRegionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateRegionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Region.Create(
            RegionId.Create(request.Id),
            RegionName.Create(request.Name),
            RegionType.Create(request.Type),
            RegionCuntryId.Create(request.CountryId)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

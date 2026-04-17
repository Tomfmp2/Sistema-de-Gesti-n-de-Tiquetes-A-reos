using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.UseCases;

public interface IUpdateCityUseCase
{
    Task ExecuteAsync(
        UpdateCityRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateCityUseCase : IUpdateCityUseCase
{
    private readonly ICityRepository _repository;

    public UpdateCityUseCase(ICityRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateCityRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = City.Create(
            CityId.Create(request.Id),
            CityName.Create(request.Name),
            CityRegionId.Create(request.RegionId)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

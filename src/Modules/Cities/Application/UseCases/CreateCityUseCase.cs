using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.UseCases;

public interface ICreateCityUseCase
{
    Task<City> ExecuteAsync(
        CreateCityRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateCityUseCase : ICreateCityUseCase
{
    private readonly ICityRepository _repository;

    public CreateCityUseCase(ICityRepository repository)
    {
        _repository = repository;
    }

    public Task<City> ExecuteAsync(
        CreateCityRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = City.CreateNew(
            CityName.Create(request.Name),
            CityRegionId.Create(request.RegionId)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}

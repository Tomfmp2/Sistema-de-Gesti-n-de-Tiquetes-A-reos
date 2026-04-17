using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Application.Services;

public sealed class CityService : ICityService
{
    private readonly ICreateCityUseCase _create;
    private readonly IGetCityByIdUseCase _getById;
    private readonly IGetAllCitiesUseCase _getAll;
    private readonly IUpdateCityUseCase _update;
    private readonly IDeleteCityUseCase _delete;

    public CityService(
        ICreateCityUseCase create,
        IGetCityByIdUseCase getById,
        IGetAllCitiesUseCase getAll,
        IUpdateCityUseCase update,
        IDeleteCityUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<City> CreateAsync(
        CreateCityRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<City?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<City>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateCityRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

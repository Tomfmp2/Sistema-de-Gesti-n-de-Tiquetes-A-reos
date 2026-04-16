using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Services;

public sealed class CountryService : ICountryService
{
    private readonly ICreateCountryUseCase _create;
    private readonly IGetCountryByIdUseCase _getById;
    private readonly IGetAllCountriesUseCase _getAll;
    private readonly IUpdateCountryUseCase _update;
    private readonly IDeleteCountryUseCase _delete;

    public CountryService(
        ICreateCountryUseCase create,
        IGetCountryByIdUseCase getById,
        IGetAllCountriesUseCase getAll,
        IUpdateCountryUseCase update,
        IDeleteCountryUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Country> CreateAsync(
        CreateCountryRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Country?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Country>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateCountryRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

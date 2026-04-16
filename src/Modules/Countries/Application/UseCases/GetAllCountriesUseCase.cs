using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.UseCases;

public interface IGetAllCountriesUseCase
{
    Task<IReadOnlyList<Country>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllCountriesUseCase : IGetAllCountriesUseCase
{
    private readonly ICountryRepository _repository;

    public GetAllCountriesUseCase(ICountryRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Country>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

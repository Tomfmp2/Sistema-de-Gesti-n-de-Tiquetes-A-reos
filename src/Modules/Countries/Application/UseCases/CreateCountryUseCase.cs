using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.UseCases;

public interface ICreateCountryUseCase
{
    Task<Country> ExecuteAsync(
        CreateCountryRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateCountryUseCase : ICreateCountryUseCase
{
    private readonly ICountryRepository _repository;

    public CreateCountryUseCase(ICountryRepository repository)
    {
        _repository = repository;
    }

    public Task<Country> ExecuteAsync(
        CreateCountryRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Country.CreateNew(
            CountryName.Create(request.Name),
            CountryCodigoIso.Create(request.CodeIso.Trim()),
            CountryContinentId.Create(request.ContinentId)
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}

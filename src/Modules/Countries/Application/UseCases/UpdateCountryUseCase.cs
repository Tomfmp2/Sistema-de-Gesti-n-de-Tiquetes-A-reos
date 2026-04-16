using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.UseCases;

public interface IUpdateCountryUseCase
{
    Task ExecuteAsync(
        UpdateCountryRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateCountryUseCase : IUpdateCountryUseCase
{
    private readonly ICountryRepository _repository;

    public UpdateCountryUseCase(ICountryRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateCountryRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Country.Create(
            CountryId.Create(request.Id),
            CountryName.Create(request.Name),
            CountryCodigoIso.Create(request.CodeIso.Trim()),
            CountryContinentId.Create(request.ContinentId)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

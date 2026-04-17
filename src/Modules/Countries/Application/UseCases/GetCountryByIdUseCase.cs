using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.UseCases;

public interface IGetCountryByIdUseCase
{
    Task<Country?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetCountryByIdUseCase : IGetCountryByIdUseCase
{
    private readonly ICountryRepository _repository;

    public GetCountryByIdUseCase(ICountryRepository repository)
    {
        _repository = repository;
    }

    public Task<Country?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Country?>(null);
        }

        return _repository.GetByIdAsync(CountryId.Create(id), cancellationToken);
    }
}

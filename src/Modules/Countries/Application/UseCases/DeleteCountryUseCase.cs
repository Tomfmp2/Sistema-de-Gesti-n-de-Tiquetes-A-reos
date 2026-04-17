using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Application.UseCases;

public interface IDeleteCountryUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteCountryUseCase : IDeleteCountryUseCase
{
    private readonly ICountryRepository _repository;

    public DeleteCountryUseCase(ICountryRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(CountryId.Create(id), cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;

public interface IGetFareByIdUseCase
{
    Task<Fare?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetFareByIdUseCase : IGetFareByIdUseCase
{
    private readonly IFareRepository _repository;

    public GetFareByIdUseCase(IFareRepository repository)
    {
        _repository = repository;
    }

    public Task<Fare?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Fare?>(null);
        }

        return _repository.GetByIdAsync(FareId.Create(id), cancellationToken);
    }
}

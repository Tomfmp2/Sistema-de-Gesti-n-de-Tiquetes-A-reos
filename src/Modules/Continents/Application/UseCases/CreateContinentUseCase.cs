using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.UseCases;

public interface ICreateContinentUseCase
{
    Task<Continent> ExecuteAsync(
        CreateContinentRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateContinentUseCase : ICreateContinentUseCase
{
    private readonly IContinentRepository _repository;

    public CreateContinentUseCase(IContinentRepository repository)
    {
        _repository = repository;
    }

    public Task<Continent> ExecuteAsync(
        CreateContinentRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Continent.CreateNew(ContinentName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

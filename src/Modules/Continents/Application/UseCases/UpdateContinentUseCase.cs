using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.UseCases;

public interface IUpdateContinentUseCase
{
    Task ExecuteAsync(
        UpdateContinentRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateContinentUseCase : IUpdateContinentUseCase
{
    private readonly IContinentRepository _repository;

    public UpdateContinentUseCase(IContinentRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateContinentRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Continent.Create(
            ContinentId.Create(request.Id),
            ContinentName.Create(request.Name)
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

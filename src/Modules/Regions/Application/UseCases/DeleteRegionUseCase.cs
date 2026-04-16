using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.UseCases;

public interface IDeleteRegionUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteRegionUseCase : IDeleteRegionUseCase
{
    private readonly IRegionRepository _repository;

    public DeleteRegionUseCase(IRegionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(RegionId.Create(id), cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Application.Services;

public sealed class RegionService : IRegionService
{
    private readonly ICreateRegionUseCase _create;
    private readonly IGetRegionByIdUseCase _getById;
    private readonly IGetAllRegionsUseCase _getAll;
    private readonly IUpdateRegionUseCase _update;
    private readonly IDeleteRegionUseCase _delete;

    public RegionService(
        ICreateRegionUseCase create,
        IGetRegionByIdUseCase getById,
        IGetAllRegionsUseCase getAll,
        IUpdateRegionUseCase update,
        IDeleteRegionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Region> CreateAsync(
        CreateRegionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Region?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Region>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateRegionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

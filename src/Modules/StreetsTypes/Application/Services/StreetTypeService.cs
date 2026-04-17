using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Application.Services;

public sealed class StreetTypeService : IStreetTypeService
{
    private readonly ICreateStreetTypeUseCase _create;
    private readonly IGetStreetTypeByIdUseCase _getById;
    private readonly IGetAllStreetTypesUseCase _getAll;
    private readonly IUpdateStreetTypeUseCase _update;
    private readonly IDeleteStreetTypeUseCase _delete;

    public StreetTypeService(
        ICreateStreetTypeUseCase create,
        IGetStreetTypeByIdUseCase getById,
        IGetAllStreetTypesUseCase getAll,
        IUpdateStreetTypeUseCase update,
        IDeleteStreetTypeUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<StreetType> CreateAsync(
        CreateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<StreetType?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<StreetType>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateStreetTypeRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

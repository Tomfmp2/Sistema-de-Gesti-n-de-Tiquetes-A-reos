using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Application.Services;

public sealed class ContinentService : IContinentService
{
    private readonly ICreateContinentUseCase _create;
    private readonly IGetContinentByIdUseCase _getById;
    private readonly IGetAllContinentsUseCase _getAll;
    private readonly IUpdateContinentUseCase _update;
    private readonly IDeleteContinentUseCase _delete;

    public ContinentService(
        ICreateContinentUseCase create,
        IGetContinentByIdUseCase getById,
        IGetAllContinentsUseCase getAll,
        IUpdateContinentUseCase update,
        IDeleteContinentUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Continent> CreateAsync(
        CreateContinentRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Continent?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Continent>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateContinentRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

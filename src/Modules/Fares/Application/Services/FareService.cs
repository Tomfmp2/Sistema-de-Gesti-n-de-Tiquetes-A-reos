using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Services;

public sealed class FareService : IFareService
{
    private readonly ICreateFareUseCase _create;
    private readonly IGetFareByIdUseCase _getById;
    private readonly IGetAllFaresUseCase _getAll;
    private readonly IUpdateFareUseCase _update;
    private readonly IDeleteFareUseCase _delete;

    public FareService(
        ICreateFareUseCase create,
        IGetFareByIdUseCase getById,
        IGetAllFaresUseCase getAll,
        IUpdateFareUseCase update,
        IDeleteFareUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Fare> CreateAsync(
        CreateFareRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Fare?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Fare>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateFareRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

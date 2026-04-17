using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Services;

public sealed class CheckinService : ICheckinService
{
    private readonly ICreateCheckinUseCase _create;
    private readonly IGetCheckinByIdUseCase _getById;
    private readonly IGetAllCheckinsUseCase _getAll;
    private readonly IUpdateCheckinUseCase _update;
    private readonly IDeleteCheckinUseCase _delete;

    public CheckinService(
        ICreateCheckinUseCase create,
        IGetCheckinByIdUseCase getById,
        IGetAllCheckinsUseCase getAll,
        IUpdateCheckinUseCase update,
        IDeleteCheckinUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Checkin> CreateAsync(
        CreateCheckinRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Checkin?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Checkin>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateCheckinRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

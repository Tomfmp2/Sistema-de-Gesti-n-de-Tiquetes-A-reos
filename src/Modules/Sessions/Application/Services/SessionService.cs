using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Services;

public sealed class SessionService : ISessionService
{
    private readonly ICreateSessionUseCase _create;
    private readonly IGetSessionByIdUseCase _getById;
    private readonly IGetAllSessionsUseCase _getAll;
    private readonly IUpdateSessionUseCase _update;
    private readonly IDeleteSessionUseCase _delete;

    public SessionService(
        ICreateSessionUseCase create,
        IGetSessionByIdUseCase getById,
        IGetAllSessionsUseCase getAll,
        IUpdateSessionUseCase update,
        IDeleteSessionUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Session> CreateAsync(
        CreateSessionRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Session?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Session>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateSessionRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

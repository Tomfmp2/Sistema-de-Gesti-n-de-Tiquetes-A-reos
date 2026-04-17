using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.UseCases;

public interface IGetAllSessionsUseCase
{
    Task<IReadOnlyList<Session>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllSessionsUseCase : IGetAllSessionsUseCase
{
    private readonly ISessionRepository _repository;

    public GetAllSessionsUseCase(ISessionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Session>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

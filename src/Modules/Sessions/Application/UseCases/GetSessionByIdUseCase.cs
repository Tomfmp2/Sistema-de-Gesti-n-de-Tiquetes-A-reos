using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.UseCases;

public interface IGetSessionByIdUseCase
{
    Task<Session?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetSessionByIdUseCase : IGetSessionByIdUseCase
{
    private readonly ISessionRepository _repository;

    public GetSessionByIdUseCase(ISessionRepository repository)
    {
        _repository = repository;
    }

    public Task<Session?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Session?>(null);
        }

        return _repository.GetByIdAsync(SessionId.Create(id), cancellationToken);
    }
}

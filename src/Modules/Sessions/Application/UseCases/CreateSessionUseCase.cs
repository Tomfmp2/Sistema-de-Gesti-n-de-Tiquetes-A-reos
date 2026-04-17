using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.UseCases;

public interface ICreateSessionUseCase
{
    Task<Session> ExecuteAsync(
        CreateSessionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateSessionUseCase : ICreateSessionUseCase
{
    private readonly ISessionRepository _repository;

    public CreateSessionUseCase(ISessionRepository repository)
    {
        _repository = repository;
    }

    public Task<Session> ExecuteAsync(
        CreateSessionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Session.CreateNew(
            SessionUserId.Create(request.UserId),
            request.StartedAt,
            request.ClosedAt,
            request.OriginIp,
            request.IsActive
        );
        return _repository.AddAsync(x, cancellationToken);
    }
}

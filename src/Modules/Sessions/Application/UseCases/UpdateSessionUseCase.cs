using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Sessions.Application.UseCases;

public interface IUpdateSessionUseCase
{
    Task ExecuteAsync(
        UpdateSessionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateSessionUseCase : IUpdateSessionUseCase
{
    private readonly ISessionRepository _repository;

    public UpdateSessionUseCase(ISessionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateSessionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Session.Create(
            SessionId.Create(request.Id),
            SessionUserId.Create(request.UserId),
            request.StartedAt,
            request.ClosedAt,
            request.OriginIp,
            request.IsActive
        );
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

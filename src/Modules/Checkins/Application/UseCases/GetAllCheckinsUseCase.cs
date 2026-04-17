using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;

public interface IGetAllCheckinsUseCase
{
    Task<IReadOnlyList<Checkin>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllCheckinsUseCase : IGetAllCheckinsUseCase
{
    private readonly ICheckinRepository _repository;

    public GetAllCheckinsUseCase(ICheckinRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Checkin>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

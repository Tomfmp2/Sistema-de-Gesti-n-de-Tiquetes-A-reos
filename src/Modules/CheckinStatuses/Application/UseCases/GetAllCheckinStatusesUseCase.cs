using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.UseCases;

public interface IGetAllCheckinStatusesUseCase
{
    Task<IReadOnlyList<CheckinStatus>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllCheckinStatusesUseCase : IGetAllCheckinStatusesUseCase
{
    private readonly ICheckinStatusRepository _repository;

    public GetAllCheckinStatusesUseCase(ICheckinStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<CheckinStatus>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

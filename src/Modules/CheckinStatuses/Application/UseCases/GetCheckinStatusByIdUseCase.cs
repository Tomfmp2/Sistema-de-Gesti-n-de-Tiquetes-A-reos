using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.UseCases;

public interface IGetCheckinStatusByIdUseCase
{
    Task<CheckinStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetCheckinStatusByIdUseCase : IGetCheckinStatusByIdUseCase
{
    private readonly ICheckinStatusRepository _repository;

    public GetCheckinStatusByIdUseCase(ICheckinStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<CheckinStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<CheckinStatus?>(null);
        }

        return _repository.GetByIdAsync(CheckinStatusId.Create(id), cancellationToken);
    }
}

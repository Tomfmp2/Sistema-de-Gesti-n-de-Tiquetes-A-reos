using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.UseCases;

public interface IUpdateCheckinStatusUseCase
{
    Task ExecuteAsync(
        UpdateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateCheckinStatusUseCase : IUpdateCheckinStatusUseCase
{
    private readonly ICheckinStatusRepository _repository;

    public UpdateCheckinStatusUseCase(ICheckinStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = CheckinStatus.Create(CheckinStatusId.Create(request.Id), CheckinStatusName.Create(request.Name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

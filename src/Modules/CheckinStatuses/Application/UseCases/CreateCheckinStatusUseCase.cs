using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Application.UseCases;

public interface ICreateCheckinStatusUseCase
{
    Task<CheckinStatus> ExecuteAsync(
        CreateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateCheckinStatusUseCase : ICreateCheckinStatusUseCase
{
    private readonly ICheckinStatusRepository _repository;

    public CreateCheckinStatusUseCase(ICheckinStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<CheckinStatus> ExecuteAsync(
        CreateCheckinStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = CheckinStatus.Create(new CheckinStatusId(0), CheckinStatusName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

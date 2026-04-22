using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.UseCases;

public interface IUpdateReservationStatusUseCase
{
    Task ExecuteAsync(
        UpdateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateReservationStatusUseCase : IUpdateReservationStatusUseCase
{
    private readonly IReservationStatusRepository _repository;

    public UpdateReservationStatusUseCase(IReservationStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = ReservationStatus.Create(ReservationStatusId.Create(request.Id), ReservationStatusName.Create(name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

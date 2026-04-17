using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.UseCases;

public interface ICreateReservationStatusUseCase
{
    Task<ReservationStatus> ExecuteAsync(
        CreateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateReservationStatusUseCase : ICreateReservationStatusUseCase
{
    private readonly IReservationStatusRepository _repository;

    public CreateReservationStatusUseCase(IReservationStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationStatus> ExecuteAsync(
        CreateReservationStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationStatus.Create(new ReservationStatusId(0), ReservationStatusName.Create(request.Name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

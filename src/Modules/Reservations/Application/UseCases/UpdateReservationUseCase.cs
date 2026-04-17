using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;

public interface IUpdateReservationUseCase
{
    Task ExecuteAsync(
        UpdateReservationRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateReservationUseCase : IUpdateReservationUseCase
{
    private readonly IReservationRepository _repository;

    public UpdateReservationUseCase(IReservationRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateReservationRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Reservation.Create(ReservationId.Create(request.Id), ReservationClientId.Create(request.ClientId), ReservationDate.Create(request.ReservationDate), ReservationStatusId.Create(request.ReservationStatusId), ReservationTotalValue.Create(request.TotalValue), ReservationExpiresAt.Create(request.ExpiresAt), ReservationCreatedAt.Create(request.CreatedAt), ReservationUpdatedAt.Create(request.UpdatedAt));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

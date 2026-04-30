using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;

public interface ICreateReservationUseCase
{
    Task<Reservation> ExecuteAsync(
        CreateReservationRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateReservationUseCase : ICreateReservationUseCase
{
    private readonly IReservationRepository _repository;

    public CreateReservationUseCase(IReservationRepository repository)
    {
        _repository = repository;
    }

    public Task<Reservation> ExecuteAsync(
        CreateReservationRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Reservation.Create(new ReservationId(0), ReservationClientId.Create(request.ClientId), ReservationDate.Create(request.ReservationDate), ReservationStatusId.Create(request.ReservationStatusId), ReservationTotalValue.Create(request.TotalValue), request.DiscountPercentage, request.OriginalTotalValue, ReservationExpiresAt.Create(request.ExpiresAt), ReservationCreatedAt.Create(request.CreatedAt), ReservationUpdatedAt.Create(request.UpdatedAt));
        return _repository.AddAsync(x, cancellationToken);
    }
}

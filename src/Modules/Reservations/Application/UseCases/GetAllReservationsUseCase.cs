using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;

public interface IGetAllReservationsUseCase
{
    Task<IReadOnlyList<Reservation>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllReservationsUseCase : IGetAllReservationsUseCase
{
    private readonly IReservationRepository _repository;

    public GetAllReservationsUseCase(IReservationRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Reservation>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

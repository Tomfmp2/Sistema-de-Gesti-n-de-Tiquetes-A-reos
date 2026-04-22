using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;

public sealed class GetReservationsByClientIdUseCase
{
    private readonly IReservationRepository _repository;

    public GetReservationsByClientIdUseCase(IReservationRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Reservation>> ExecuteAsync(
        int clientId,
        CancellationToken cancellationToken = default
    ) => _repository.GetAllByClientIdAsync(clientId, cancellationToken);
}


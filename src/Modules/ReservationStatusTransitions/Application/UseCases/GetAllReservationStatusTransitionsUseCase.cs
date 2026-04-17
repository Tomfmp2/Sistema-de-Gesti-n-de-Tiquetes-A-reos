using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.UseCases;

public interface IGetAllReservationStatusTransitionsUseCase
{
    Task<IReadOnlyList<ReservationStatusTransition>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllReservationStatusTransitionsUseCase : IGetAllReservationStatusTransitionsUseCase
{
    private readonly IReservationStatusTransitionRepository _repository;

    public GetAllReservationStatusTransitionsUseCase(IReservationStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<ReservationStatusTransition>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

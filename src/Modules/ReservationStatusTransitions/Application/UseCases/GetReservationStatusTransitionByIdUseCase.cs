using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.UseCases;

public interface IGetReservationStatusTransitionByIdUseCase
{
    Task<ReservationStatusTransition?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetReservationStatusTransitionByIdUseCase : IGetReservationStatusTransitionByIdUseCase
{
    private readonly IReservationStatusTransitionRepository _repository;

    public GetReservationStatusTransitionByIdUseCase(IReservationStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationStatusTransition?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<ReservationStatusTransition?>(null);
        }

        return _repository.GetByIdAsync(ReservationStatusTransitionId.Create(id), cancellationToken);
    }
}

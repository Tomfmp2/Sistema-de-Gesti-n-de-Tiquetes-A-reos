using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.UseCases;

public interface IUpdateReservationStatusTransitionUseCase
{
    Task ExecuteAsync(
        UpdateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateReservationStatusTransitionUseCase : IUpdateReservationStatusTransitionUseCase
{
    private readonly IReservationStatusTransitionRepository _repository;

    public UpdateReservationStatusTransitionUseCase(IReservationStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationStatusTransition.Create(ReservationStatusTransitionId.Create(request.Id), ReservationStatusTransitionOriginStatusId.Create(request.OriginStatusId), ReservationStatusTransitionDestinationStatusId.Create(request.DestinationStatusId));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

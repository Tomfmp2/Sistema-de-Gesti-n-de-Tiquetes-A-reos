using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.UseCases;

public interface ICreateReservationStatusTransitionUseCase
{
    Task<ReservationStatusTransition> ExecuteAsync(
        CreateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateReservationStatusTransitionUseCase : ICreateReservationStatusTransitionUseCase
{
    private readonly IReservationStatusTransitionRepository _repository;

    public CreateReservationStatusTransitionUseCase(IReservationStatusTransitionRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationStatusTransition> ExecuteAsync(
        CreateReservationStatusTransitionRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = ReservationStatusTransition.Create(new ReservationStatusTransitionId(0), ReservationStatusTransitionOriginStatusId.Create(request.OriginStatusId), ReservationStatusTransitionDestinationStatusId.Create(request.DestinationStatusId));
        return _repository.AddAsync(x, cancellationToken);
    }
}

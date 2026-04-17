using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.UseCases;

public interface IGetAllReservationStatusesUseCase
{
    Task<IReadOnlyList<ReservationStatus>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllReservationStatusesUseCase : IGetAllReservationStatusesUseCase
{
    private readonly IReservationStatusRepository _repository;

    public GetAllReservationStatusesUseCase(IReservationStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<ReservationStatus>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

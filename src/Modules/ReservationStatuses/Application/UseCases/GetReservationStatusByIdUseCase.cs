using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Application.UseCases;

public interface IGetReservationStatusByIdUseCase
{
    Task<ReservationStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetReservationStatusByIdUseCase : IGetReservationStatusByIdUseCase
{
    private readonly IReservationStatusRepository _repository;

    public GetReservationStatusByIdUseCase(IReservationStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<ReservationStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<ReservationStatus?>(null);
        }

        return _repository.GetByIdAsync(ReservationStatusId.Create(id), cancellationToken);
    }
}

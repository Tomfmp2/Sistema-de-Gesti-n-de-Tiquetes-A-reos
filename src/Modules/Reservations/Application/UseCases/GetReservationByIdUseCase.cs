using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.UseCases;

public interface IGetReservationByIdUseCase
{
    Task<Reservation?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetReservationByIdUseCase : IGetReservationByIdUseCase
{
    private readonly IReservationRepository _repository;

    public GetReservationByIdUseCase(IReservationRepository repository)
    {
        _repository = repository;
    }

    public Task<Reservation?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Reservation?>(null);
        }

        return _repository.GetByIdAsync(ReservationId.Create(id), cancellationToken);
    }
}

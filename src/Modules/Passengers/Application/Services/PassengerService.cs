using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Services;

public sealed class PassengerService : IPassengerService
{
    private readonly ICreatePassengerUseCase _create;
    private readonly IGetPassengerByIdUseCase _getById;
    private readonly IGetAllPassengersUseCase _getAll;
    private readonly IUpdatePassengerUseCase _update;
    private readonly IDeletePassengerUseCase _delete;

    public PassengerService(
        ICreatePassengerUseCase create,
        IGetPassengerByIdUseCase getById,
        IGetAllPassengersUseCase getAll,
        IUpdatePassengerUseCase update,
        IDeletePassengerUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Passenger> CreateAsync(
        CreatePassengerRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Passenger?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Passenger>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdatePassengerRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

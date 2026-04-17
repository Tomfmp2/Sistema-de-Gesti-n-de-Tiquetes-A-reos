using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Services;

public sealed class FlightRoleService : IFlightRoleService
{
    private readonly ICreateFlightRoleUseCase _create;
    private readonly IGetFlightRoleByIdUseCase _getById;
    private readonly IGetAllFlightRolesUseCase _getAll;
    private readonly IUpdateFlightRoleUseCase _update;
    private readonly IDeleteFlightRoleUseCase _delete;

    public FlightRoleService(
        ICreateFlightRoleUseCase create,
        IGetFlightRoleByIdUseCase getById,
        IGetAllFlightRolesUseCase getAll,
        IUpdateFlightRoleUseCase update,
        IDeleteFlightRoleUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<FlightRole> CreateAsync(CreateFlightRoleRequest request, CancellationToken cancellationToken = default) =>
        _create.ExecuteAsync(request, cancellationToken);

    public Task<FlightRole?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<FlightRole>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(UpdateFlightRoleRequest request, CancellationToken cancellationToken = default) =>
        _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

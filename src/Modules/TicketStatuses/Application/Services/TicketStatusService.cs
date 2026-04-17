using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Services;

public sealed class TicketStatusService : ITicketStatusService
{
    private readonly ICreateTicketStatusUseCase _create;
    private readonly IGetTicketStatusByIdUseCase _getById;
    private readonly IGetAllTicketStatusesUseCase _getAll;
    private readonly IUpdateTicketStatusUseCase _update;
    private readonly IDeleteTicketStatusUseCase _delete;

    public TicketStatusService(
        ICreateTicketStatusUseCase create,
        IGetTicketStatusByIdUseCase getById,
        IGetAllTicketStatusesUseCase getAll,
        IUpdateTicketStatusUseCase update,
        IDeleteTicketStatusUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<TicketStatus> CreateAsync(
        CreateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<TicketStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<TicketStatus>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

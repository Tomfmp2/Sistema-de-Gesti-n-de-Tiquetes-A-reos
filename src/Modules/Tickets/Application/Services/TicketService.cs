using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Services;

public sealed class TicketService : ITicketService
{
    private readonly ICreateTicketUseCase _create;
    private readonly IGetTicketByIdUseCase _getById;
    private readonly IGetAllTicketsUseCase _getAll;
    private readonly IUpdateTicketUseCase _update;
    private readonly IDeleteTicketUseCase _delete;

    public TicketService(
        ICreateTicketUseCase create,
        IGetTicketByIdUseCase getById,
        IGetAllTicketsUseCase getAll,
        IUpdateTicketUseCase update,
        IDeleteTicketUseCase delete
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
    }

    public Task<Ticket> CreateAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default
    ) => _create.ExecuteAsync(request, cancellationToken);

    public Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        _getById.ExecuteAsync(id, cancellationToken);

    public Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default) =>
        _getAll.ExecuteAsync(cancellationToken);

    public Task UpdateAsync(
        UpdateTicketRequest request,
        CancellationToken cancellationToken = default
    ) => _update.ExecuteAsync(request, cancellationToken);

    public Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        _delete.ExecuteAsync(id, cancellationToken);
}

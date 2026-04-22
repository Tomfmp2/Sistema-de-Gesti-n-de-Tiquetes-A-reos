using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;

public interface IUpdateTicketUseCase
{
    Task ExecuteAsync(
        UpdateTicketRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateTicketUseCase : IUpdateTicketUseCase
{
    private readonly ITicketRepository _repository;

    public UpdateTicketUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateTicketRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var code = request.Code;
        ArgumentNullException.ThrowIfNull(code);
        var x = Ticket.Create(TicketId.Create(request.Id), TicketReservationPassengerId.Create(request.ReservationPassengerId), TicketCode.Create(code), TicketIssueDate.Create(request.IssueDate), TicketStatusId.Create(request.TicketStatusId), TicketCreatedAt.Create(request.CreatedAt), TicketUpdatedAt.Create(request.UpdatedAt));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

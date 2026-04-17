using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;

public interface ICreateTicketUseCase
{
    Task<Ticket> ExecuteAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateTicketUseCase : ICreateTicketUseCase
{
    private readonly ITicketRepository _repository;

    public CreateTicketUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task<Ticket> ExecuteAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Ticket.Create(new TicketId(0), TicketReservationPassengerId.Create(request.ReservationPassengerId), TicketCode.Create(request.Code), TicketIssueDate.Create(request.IssueDate), TicketStatusId.Create(request.TicketStatusId), TicketCreatedAt.Create(request.CreatedAt), TicketUpdatedAt.Create(request.UpdatedAt));
        return _repository.AddAsync(x, cancellationToken);
    }
}

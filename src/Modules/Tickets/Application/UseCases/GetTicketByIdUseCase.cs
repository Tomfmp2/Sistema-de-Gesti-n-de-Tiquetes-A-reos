using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;

public interface IGetTicketByIdUseCase
{
    Task<Ticket?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetTicketByIdUseCase : IGetTicketByIdUseCase
{
    private readonly ITicketRepository _repository;

    public GetTicketByIdUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task<Ticket?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<Ticket?>(null);
        }

        return _repository.GetByIdAsync(TicketId.Create(id), cancellationToken);
    }
}

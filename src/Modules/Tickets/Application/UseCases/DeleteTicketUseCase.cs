using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;

public interface IDeleteTicketUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteTicketUseCase : IDeleteTicketUseCase
{
    private readonly ITicketRepository _repository;

    public DeleteTicketUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(TicketId.Create(id), cancellationToken);
    }
}

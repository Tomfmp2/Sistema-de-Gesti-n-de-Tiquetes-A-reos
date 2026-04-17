using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.UseCases;

public interface IDeleteTicketStatusUseCase
{
    Task ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class DeleteTicketStatusUseCase : IDeleteTicketStatusUseCase
{
    private readonly ITicketStatusRepository _repository;

    public DeleteTicketStatusUseCase(ITicketStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.CompletedTask;
        }

        return _repository.DeleteAsync(TicketStatusId.Create(id), cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;

public interface IGetAllTicketsUseCase
{
    Task<IReadOnlyList<Ticket>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllTicketsUseCase : IGetAllTicketsUseCase
{
    private readonly ITicketRepository _repository;

    public GetAllTicketsUseCase(ITicketRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Ticket>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

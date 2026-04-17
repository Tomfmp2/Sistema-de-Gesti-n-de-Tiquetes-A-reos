using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.UseCases;

public interface IGetAllTicketStatusesUseCase
{
    Task<IReadOnlyList<TicketStatus>> ExecuteAsync(CancellationToken cancellationToken = default);
}

public sealed class GetAllTicketStatusesUseCase : IGetAllTicketStatusesUseCase
{
    private readonly ITicketStatusRepository _repository;

    public GetAllTicketStatusesUseCase(ITicketStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<TicketStatus>> ExecuteAsync(
        CancellationToken cancellationToken = default
    ) => _repository.GetAllAsync(cancellationToken);
}

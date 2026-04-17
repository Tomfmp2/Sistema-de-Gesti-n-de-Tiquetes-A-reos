using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.UseCases;

public interface IGetTicketStatusByIdUseCase
{
    Task<TicketStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default);
}

public sealed class GetTicketStatusByIdUseCase : IGetTicketStatusByIdUseCase
{
    private readonly ITicketStatusRepository _repository;

    public GetTicketStatusByIdUseCase(ITicketStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<TicketStatus?> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id < 1)
        {
            return Task.FromResult<TicketStatus?>(null);
        }

        return _repository.GetByIdAsync(TicketStatusId.Create(id), cancellationToken);
    }
}

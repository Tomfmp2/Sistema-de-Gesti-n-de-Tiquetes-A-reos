using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.UseCases;

public interface ICreateTicketStatusUseCase
{
    Task<TicketStatus> ExecuteAsync(
        CreateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateTicketStatusUseCase : ICreateTicketStatusUseCase
{
    private readonly ITicketStatusRepository _repository;

    public CreateTicketStatusUseCase(ITicketStatusRepository repository)
    {
        _repository = repository;
    }

    public Task<TicketStatus> ExecuteAsync(
        CreateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var name = request.Name;
        ArgumentNullException.ThrowIfNull(name);
        var x = TicketStatus.Create(new TicketStatusId(0), TicketStatusName.Create(name));
        return _repository.AddAsync(x, cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.UseCases;

public interface IUpdateTicketStatusUseCase
{
    Task ExecuteAsync(
        UpdateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateTicketStatusUseCase : IUpdateTicketStatusUseCase
{
    private readonly ITicketStatusRepository _repository;

    public UpdateTicketStatusUseCase(ITicketStatusRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = TicketStatus.Create(TicketStatusId.Create(request.Id), TicketStatusName.Create(request.Name));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

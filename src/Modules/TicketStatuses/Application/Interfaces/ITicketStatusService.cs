using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Application.Interfaces;

public interface ITicketStatusService
{
    Task<TicketStatus> CreateAsync(
        CreateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task<TicketStatus?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TicketStatus>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateTicketStatusRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

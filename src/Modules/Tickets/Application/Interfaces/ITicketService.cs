using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Interfaces;

public interface ITicketService
{
    Task<Ticket> CreateAsync(
        CreateTicketRequest request,
        CancellationToken cancellationToken = default
    );

    Task<Ticket?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(
        UpdateTicketRequest request,
        CancellationToken cancellationToken = default
    );

    Task DeleteAsync(int id, CancellationToken cancellationToken = default);
}

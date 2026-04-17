using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.Repositories;

public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(TicketId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Ticket>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Ticket> AddAsync(Ticket entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(Ticket entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TicketId id, CancellationToken cancellationToken = default);
}

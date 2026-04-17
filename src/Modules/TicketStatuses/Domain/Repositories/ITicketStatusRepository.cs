using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.Repositories;

public interface ITicketStatusRepository
{
    Task<TicketStatus?> GetByIdAsync(TicketStatusId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TicketStatus>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<TicketStatus> AddAsync(TicketStatus entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(TicketStatus entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(TicketStatusId id, CancellationToken cancellationToken = default);
}

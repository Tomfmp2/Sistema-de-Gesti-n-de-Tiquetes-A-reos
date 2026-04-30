using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Repositories;

public interface IMilesTransactionRepository
{
    Task<MilesTransaction> AddAsync(MilesTransaction entity, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MilesTransaction>> GetByClientIdAsync(int clientId, CancellationToken cancellationToken = default);
}

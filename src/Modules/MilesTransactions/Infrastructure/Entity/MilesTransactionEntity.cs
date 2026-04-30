using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Enum;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Infrastructure.Entity;

public class MilesTransactionEntity
{
    public int Id { get; set; }
    public int ClientId { get; set; }
    public ClientEntity? Client { get; set; }

    public int? ReservationId { get; set; }
    public ReservationEntity? Reservation { get; set; }

    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
    public DateTime TransactionDate { get; set; }
}

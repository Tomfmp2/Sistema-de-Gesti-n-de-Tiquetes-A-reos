using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Enum;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Domain.ValueObjet;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Domain.Aggregate;

public sealed class MilesTransaction
{
    public int Id { get; private set; }
    public ClientId ClientId { get; private set; }
    public ReservationId? ReservationId { get; private set; }
    public decimal Amount { get; private set; }
    public TransactionType TransactionType { get; private set; }
    public DateTime TransactionDate { get; private set; }

    private MilesTransaction(
        int id,
        ClientId clientId,
        ReservationId? reservationId,
        decimal amount,
        TransactionType transactionType,
        DateTime transactionDate)
    {
        Id = id;
        ClientId = clientId;
        ReservationId = reservationId;
        Amount = amount;
        TransactionType = transactionType;
        TransactionDate = transactionDate;
    }

    public static MilesTransaction CreateAccumulation(ClientId clientId, ReservationId? reservationId, decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive for accumulation.");
        
        return new MilesTransaction(
            0,
            clientId,
            reservationId,
            amount,
            TransactionType.Accumulation,
            DateTime.UtcNow
        );
    }

    public static MilesTransaction CreateRedemption(ClientId clientId, ReservationId reservationId, decimal amount)
    {
        if (amount <= 0) throw new ArgumentException("Amount must be positive for redemption.");

        return new MilesTransaction(
            0,
            clientId,
            reservationId,
            -amount, // Redemption is stored as a negative value
            TransactionType.Redemption,
            DateTime.UtcNow
        );
    }
}

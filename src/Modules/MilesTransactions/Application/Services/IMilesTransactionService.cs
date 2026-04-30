using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.DTOs;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.Services;

public interface IMilesTransactionService
{
    // Validación
    Task<bool> ClientExistsAsync(int clientId, CancellationToken cancellationToken = default);

    // Acciones Transaccionales
    Task AccumulateMilesAsync(int clientId, int reservationId, decimal milesAmount, CancellationToken cancellationToken = default);
    Task RevertMilesForReservationAsync(int reservationId, CancellationToken cancellationToken = default);
    Task RedeemMilesAsync(int clientId, int reservationId, decimal milesAmount, CancellationToken cancellationToken = default);
    Task<decimal> GetClientBalanceAsync(int clientId, CancellationToken cancellationToken = default);

    // Reportes Analíticos (LINQ)
    Task<List<ClientMilesDTO>> GetTopAccumulatorsAsync(CancellationToken cancellationToken = default);
    Task<List<ClientRedemptionDTO>> GetTopRedeemersAsync(CancellationToken cancellationToken = default);
    Task<List<AirlineLoyaltyDTO>> GetTopAirlinesByLoyaltyAsync(CancellationToken cancellationToken = default);
    Task<List<RouteMilesDTO>> GetTopRoutesByMilesAsync(CancellationToken cancellationToken = default);
    Task<List<FrequentFlyerDTO>> GetFrequentFlyersRankingAsync(CancellationToken cancellationToken = default);
}

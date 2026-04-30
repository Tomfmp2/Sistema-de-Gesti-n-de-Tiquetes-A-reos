namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Application.Dtos;

public sealed record CreateReservationRequest(int ClientId, DateTime ReservationDate, int ReservationStatusId, decimal TotalValue, decimal DiscountPercentage, decimal OriginalTotalValue, DateTime? ExpiresAt, DateTime CreatedAt, DateTime UpdatedAt);

public sealed record UpdateReservationRequest(int Id, int ClientId, DateTime ReservationDate, int ReservationStatusId, decimal TotalValue, decimal DiscountPercentage, decimal OriginalTotalValue, DateTime? ExpiresAt, DateTime CreatedAt, DateTime UpdatedAt);

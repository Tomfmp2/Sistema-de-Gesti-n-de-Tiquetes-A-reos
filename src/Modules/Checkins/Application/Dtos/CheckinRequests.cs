namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Dtos;

public sealed record CreateCheckinRequest(int TicketId, int StaffId, int FlightSeatId, DateTime CheckinDate, int CheckinStatusId, string? BoardingPassNumber, bool HasCheckedBaggage, decimal? BaggageWeightKg);

public sealed record UpdateCheckinRequest(int Id, int TicketId, int StaffId, int FlightSeatId, DateTime CheckinDate, int CheckinStatusId, string? BoardingPassNumber, bool HasCheckedBaggage, decimal? BaggageWeightKg);

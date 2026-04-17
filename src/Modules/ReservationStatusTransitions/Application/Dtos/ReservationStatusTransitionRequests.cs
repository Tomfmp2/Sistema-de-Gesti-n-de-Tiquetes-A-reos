namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Application.Dtos;

public sealed record CreateReservationStatusTransitionRequest(int OriginStatusId, int DestinationStatusId);

public sealed record UpdateReservationStatusTransitionRequest(int Id, int OriginStatusId, int DestinationStatusId);

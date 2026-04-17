namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatusTransitions.Application.Dtos;

public sealed record CreateFlightStatusTransitionRequest(int OriginStatusId, int DestinationStatusId);

public sealed record UpdateFlightStatusTransitionRequest(int Id, int OriginStatusId, int DestinationStatusId);

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Application.Dtos;

public sealed record CreateFareRequest(int RouteId, int CabinTypeId, int PassengerTypeId, int SeasonId, decimal BasePrice, DateTime? ValidFrom, DateTime? ValidTo);

public sealed record UpdateFareRequest(int Id, int RouteId, int CabinTypeId, int PassengerTypeId, int SeasonId, decimal BasePrice, DateTime? ValidFrom, DateTime? ValidTo);

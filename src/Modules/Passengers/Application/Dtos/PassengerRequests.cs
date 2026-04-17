namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Application.Dtos;

public sealed record CreatePassengerRequest(int PersonId, int PassengerTypeId);

public sealed record UpdatePassengerRequest(int Id, int PersonId, int PassengerTypeId);

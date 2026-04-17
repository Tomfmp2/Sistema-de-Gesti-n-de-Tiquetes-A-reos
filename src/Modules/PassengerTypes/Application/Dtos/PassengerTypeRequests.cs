namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Application.Dtos;

public sealed record CreatePassengerTypeRequest(string Name, int? MinAge, int? MaxAge);

public sealed record UpdatePassengerTypeRequest(int Id, string Name, int? MinAge, int? MaxAge);

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightRoles.Application.Dtos;

public sealed record CreateFlightRoleRequest(string Name);
public sealed record UpdateFlightRoleRequest(int Id, string Name);

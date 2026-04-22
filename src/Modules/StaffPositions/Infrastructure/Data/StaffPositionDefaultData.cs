using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Data;

public static class StaffPositionDefaultData
{
    public static readonly StaffPositionEntity[] StaffPositions =
    [
        new() { Id = 1, Name = "Piloto" },
        new() { Id = 2, Name = "Copiloto" },
        new() { Id = 3, Name = "Tripulante de cabina" },
        new() { Id = 4, Name = "Agente de puerta" },
        new() { Id = 5, Name = "Técnico de mantenimiento" }
    ];
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Infrastructure.Data;

public static class PassengerTypeDefaultData
{
    public static readonly PassengerTypeEntity[] PassengerTypes =
    [
        new() { Id = 1, Name = "Infante", MinAge = 0, MaxAge = 1 },
        new() { Id = 2, Name = "Niño", MinAge = 2, MaxAge = 11 },
        new() { Id = 3, Name = "Adulto", MinAge = 12, MaxAge = null },
        new() { Id = 4, Name = "Adulto mayor", MinAge = 60, MaxAge = null }
    ];
}

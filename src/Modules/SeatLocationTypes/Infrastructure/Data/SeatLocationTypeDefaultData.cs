using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Data;

public static class SeatLocationTypeDefaultData
{
    public static readonly SeatLocationTypeEntity[] SeatLocationTypes =
    [
        new() { Id = 1, Name = "Ventana" },
        new() { Id = 2, Name = "Pasillo" },
        new() { Id = 3, Name = "Centro" }
    ];
}

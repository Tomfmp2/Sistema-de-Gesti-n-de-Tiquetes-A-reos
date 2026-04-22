using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Data;

public static class ReservationStatusDefaultData
{
    public static readonly ReservationStatusEntity[] ReservationStatuses =
    [
        new() { Id = 1, Name = "Pendiente" },
        new() { Id = 2, Name = "Confirmada" },
        new() { Id = 3, Name = "Cancelada" },
        new() { Id = 4, Name = "Expirada" },
        new() { Id = 5, Name = "Completada" }
    ];
}

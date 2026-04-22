using sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Infrastructure.Data;

public static class CheckinStatusDefaultData
{
    public static readonly CheckinStatusEntity[] CheckinStatuses =
    [
        new() { Id = 1, Name = "Pendiente" },
        new() { Id = 2, Name = "Realizado" },
        new() { Id = 3, Name = "Cerrado" },
        new() { Id = 4, Name = "Cancelado" }
    ];
}

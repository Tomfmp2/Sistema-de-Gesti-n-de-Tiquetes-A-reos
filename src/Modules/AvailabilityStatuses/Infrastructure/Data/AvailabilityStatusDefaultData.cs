using sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AvailabilityStatuses.Infrastructure.Data;

public static class AvailabilityStatusDefaultData
{
    public static readonly AvailabilityStatusEntity[] AvailabilityStatuses =
    [
        new() { Id = 1, Name = "Disponible" },
        new() { Id = 2, Name = "Asignado" },
        new() { Id = 3, Name = "En descanso" },
        new() { Id = 4, Name = "Incapacitado" },
        new() { Id = 5, Name = "Vacaciones" }
    ];
}

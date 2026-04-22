using sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Infrastructure.Data;

public static class SystemRoleDefaultData
{
    public static readonly SystemRoleEntity[] SystemRoles =
    [
        new() { Id = 1, Name = "Administrador", Description = "Acceso completo al sistema" },
        new() { Id = 2, Name = "Agente", Description = "Gestión de ventas, reservas y atención al cliente" },
        new() { Id = 3, Name = "Cliente", Description = "Acceso de autoservicio para pasajeros" },
        new() { Id = 4, Name = "Operaciones", Description = "Gestión operativa de vuelos y tripulación" }
    ];
}

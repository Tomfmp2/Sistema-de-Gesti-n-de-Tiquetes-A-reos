using sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Permissions.Infrastructure.Data;

public static class PermissionDefaultData
{
    public static readonly PermissionEntity[] Permissions =
    [
        new() { Id = 1, Name = "reservations.manage", Description = "Gestionar reservas" },
        new() { Id = 2, Name = "flights.manage", Description = "Gestionar vuelos" },
        new() { Id = 3, Name = "catalogs.manage", Description = "Gestionar catálogos del sistema" },
        new() { Id = 4, Name = "payments.manage", Description = "Gestionar pagos" },
        new() { Id = 5, Name = "reports.view", Description = "Consultar reportes" }
    ];
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentStatuses.Infrastructure.Data;

public static class PaymentStatusDefaultData
{
    public static readonly PaymentStatusEntity[] PaymentStatuses =
    [
        new() { Id = 1, Name = "Pendiente" },
        new() { Id = 2, Name = "Aprobado" },
        new() { Id = 3, Name = "Rechazado" },
        new() { Id = 4, Name = "Reembolsado" },
        new() { Id = 5, Name = "Anulado" }
    ];
}

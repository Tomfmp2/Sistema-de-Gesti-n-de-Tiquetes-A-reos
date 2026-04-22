using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethodTypes.Infrastructure.Data;

public static class PaymentMethodTypeDefaultData
{
    public static readonly PaymentMethodTypeEntity[] PaymentMethodTypes =
    [
        new() { Id = 1, Name = "Tarjeta" },
        new() { Id = 2, Name = "Efectivo" },
        new() { Id = 3, Name = "Transferencia bancaria" },
        new() { Id = 4, Name = "Billetera digital" }
    ];
}

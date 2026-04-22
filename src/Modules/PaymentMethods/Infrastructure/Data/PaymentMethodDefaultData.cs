using sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PaymentMethods.Infrastructure.Data;

public static class PaymentMethodDefaultData
{
    public static readonly PaymentMethodEntity[] PaymentMethods =
    [
        new() { Id = 1, PaymentMethodTypeId = 1, CardTypeId = 1, CardIssuerId = 1, CommercialName = "Visa crédito" },
        new() { Id = 2, PaymentMethodTypeId = 1, CardTypeId = 1, CardIssuerId = 2, CommercialName = "Mastercard crédito" },
        new() { Id = 3, PaymentMethodTypeId = 1, CardTypeId = 2, CardIssuerId = 1, CommercialName = "Visa débito" },
        new() { Id = 4, PaymentMethodTypeId = 1, CardTypeId = 1, CardIssuerId = 3, CommercialName = "American Express crédito" },
        new() { Id = 5, PaymentMethodTypeId = 2, CardTypeId = null, CardIssuerId = null, CommercialName = "Efectivo en oficina" },
        new() { Id = 6, PaymentMethodTypeId = 3, CardTypeId = null, CardIssuerId = null, CommercialName = "Transferencia bancaria" },
        new() { Id = 7, PaymentMethodTypeId = 4, CardTypeId = null, CardIssuerId = null, CommercialName = "Billetera digital" }
    ];
}

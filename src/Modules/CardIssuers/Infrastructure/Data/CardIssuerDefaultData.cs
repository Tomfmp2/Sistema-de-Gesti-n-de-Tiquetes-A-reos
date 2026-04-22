using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardIssuers.Infrastructure.Data;

public static class CardIssuerDefaultData
{
    public static readonly CardIssuerEntity[] CardIssuers =
    [
        new() { Id = 1, Name = "Visa" },
        new() { Id = 2, Name = "Mastercard" },
        new() { Id = 3, Name = "American Express" },
        new() { Id = 4, Name = "Diners Club" }
    ];
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CardTypes.Infrastructure.Data;

public static class CardTypeDefaultData
{
    public static readonly CardTypeEntity[] CardTypes =
    [
        new() { Id = 1, Name = "Crédito" },
        new() { Id = 2, Name = "Débito" },
        new() { Id = 3, Name = "Prepago" }
    ];
}

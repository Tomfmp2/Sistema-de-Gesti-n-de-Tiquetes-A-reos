using sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.InvoiceItemTypes.Infrastructure.Data;

public static class InvoiceItemTypeDefaultData
{
    public static readonly InvoiceItemTypeEntity[] InvoiceItemTypes =
    [
        new() { Id = 1, Name = "Tarifa aérea" },
        new() { Id = 2, Name = "Impuestos" },
        new() { Id = 3, Name = "Equipaje" },
        new() { Id = 4, Name = "Servicio" },
        new() { Id = 5, Name = "Descuento" }
    ];
}

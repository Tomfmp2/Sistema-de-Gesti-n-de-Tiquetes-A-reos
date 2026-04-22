using sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.DocumentTypes.Infrastructure.Data;

public static class DocumentTypeDefaultData
{
    public static readonly DocumentTypeEntity[] DocumentTypes =
    [
        new() { Id = 1, Code = "CC", Name = "Cédula de ciudadanía" },
        new() { Id = 2, Code = "CE", Name = "Cédula de extranjería" },
        new() { Id = 3, Code = "PAS", Name = "Pasaporte" },
        new() { Id = 4, Code = "TI", Name = "Tarjeta de identidad" },
        new() { Id = 5, Code = "NIT", Name = "NIT" }
    ];
}

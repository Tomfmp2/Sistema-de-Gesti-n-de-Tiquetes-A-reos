using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Data;

public static class CountryDefaultData
{
    public static readonly CountryEntity[] Countries =
    [
        new() { Id = 1, Name = "Colombia", CodeIso = "COL", ContinentId = 1 },
        new() { Id = 2, Name = "Estados Unidos", CodeIso = "USA", ContinentId = 1 },
        new() { Id = 3, Name = "México", CodeIso = "MEX", ContinentId = 1 },
        new() { Id = 4, Name = "Brasil", CodeIso = "BRA", ContinentId = 1 },
        new() { Id = 5, Name = "Argentina", CodeIso = "ARG", ContinentId = 1 },
        new() { Id = 6, Name = "Chile", CodeIso = "CHL", ContinentId = 1 },
        new() { Id = 7, Name = "Perú", CodeIso = "PER", ContinentId = 1 },
        new() { Id = 8, Name = "Canadá", CodeIso = "CAN", ContinentId = 1 },
        new() { Id = 9, Name = "España", CodeIso = "ESP", ContinentId = 2 },
        new() { Id = 10, Name = "Francia", CodeIso = "FRA", ContinentId = 2 },
        new() { Id = 11, Name = "Reino Unido", CodeIso = "GBR", ContinentId = 2 },
        new() { Id = 12, Name = "Alemania", CodeIso = "DEU", ContinentId = 2 },
        new() { Id = 13, Name = "Italia", CodeIso = "ITA", ContinentId = 2 },
        new() { Id = 14, Name = "Japón", CodeIso = "JPN", ContinentId = 3 },
        new() { Id = 15, Name = "China", CodeIso = "CHN", ContinentId = 3 },
        new() { Id = 16, Name = "Corea del Sur", CodeIso = "KOR", ContinentId = 3 },
        new() { Id = 17, Name = "Australia", CodeIso = "AUS", ContinentId = 5 },
        new() { Id = 18, Name = "Sudáfrica", CodeIso = "ZAF", ContinentId = 4 }
    ];
}

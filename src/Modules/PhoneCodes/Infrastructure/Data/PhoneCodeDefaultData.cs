using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Infrastructure.Data;

public static class PhoneCodeDefaultData
{
    public static readonly PhoneCodeEntity[] PhoneCodes =
    [
        new() { Id = 1, CountryDialCode = "+57", CountryName = "Colombia" },
        new() { Id = 2, CountryDialCode = "+1", CountryName = "Estados Unidos / Canadá" },
        new() { Id = 3, CountryDialCode = "+52", CountryName = "México" },
        new() { Id = 4, CountryDialCode = "+55", CountryName = "Brasil" },
        new() { Id = 5, CountryDialCode = "+54", CountryName = "Argentina" },
        new() { Id = 6, CountryDialCode = "+56", CountryName = "Chile" },
        new() { Id = 7, CountryDialCode = "+51", CountryName = "Perú" },
        new() { Id = 8, CountryDialCode = "+34", CountryName = "España" },
        new() { Id = 9, CountryDialCode = "+33", CountryName = "Francia" },
        new() { Id = 10, CountryDialCode = "+44", CountryName = "Reino Unido" },
        new() { Id = 11, CountryDialCode = "+49", CountryName = "Alemania" },
        new() { Id = 12, CountryDialCode = "+39", CountryName = "Italia" },
        new() { Id = 13, CountryDialCode = "+81", CountryName = "Japón" },
        new() { Id = 14, CountryDialCode = "+86", CountryName = "China" },
        new() { Id = 15, CountryDialCode = "+61", CountryName = "Australia" },
        new() { Id = 16, CountryDialCode = "+27", CountryName = "Sudáfrica" }
    ];
}

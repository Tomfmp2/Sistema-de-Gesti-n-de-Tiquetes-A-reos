namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Application.Dtos;

public sealed record CreatePhoneCodeRequest(string CountryDialCode, string CountryName);

public sealed record UpdatePhoneCodeRequest(int Id, string CountryDialCode, string CountryName);

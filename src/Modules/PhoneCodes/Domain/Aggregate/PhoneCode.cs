using sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.Aggregate;

public sealed class PhoneCode
{
    public PhoneCodeId Id { get; private set; }
    public PhoneDialCode CountryDialCode { get; private set; }
    public PhoneCodeCountryLabel CountryName { get; private set; }

    private PhoneCode(PhoneCodeId id, PhoneDialCode countryDialCode, PhoneCodeCountryLabel countryName)
    {
        Id = id;
        CountryDialCode = countryDialCode;
        CountryName = countryName;
    }

    public static PhoneCode CreateNew(PhoneDialCode countryDialCode, PhoneCodeCountryLabel countryName)
    {
        return new PhoneCode(PhoneCodeId.Unpersisted, countryDialCode, countryName);
    }

    public static PhoneCode Create(
        PhoneCodeId id,
        PhoneDialCode countryDialCode,
        PhoneCodeCountryLabel countryName
    )
    {
        return new PhoneCode(id, countryDialCode, countryName);
    }
}

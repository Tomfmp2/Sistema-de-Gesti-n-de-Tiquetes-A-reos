namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

public sealed class PhoneCodeCountryLabel
{
    public string Value { get; }

    public PhoneCodeCountryLabel(string value) => Value = value;

    public static PhoneCodeCountryLabel Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre del país no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 100)
        {
            throw new ArgumentException("Máximo 100 caracteres.");
        }

        return new PhoneCodeCountryLabel(t);
    }
}

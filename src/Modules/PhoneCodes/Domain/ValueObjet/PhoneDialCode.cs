namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PhoneCodes.Domain.ValueObjet;

public sealed class PhoneDialCode
{
    public string Value { get; }

    public PhoneDialCode(string value) => Value = value;

    public static PhoneDialCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El prefijo no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 5)
        {
            throw new ArgumentException("Máximo 5 caracteres.");
        }

        return new PhoneDialCode(t);
    }
}

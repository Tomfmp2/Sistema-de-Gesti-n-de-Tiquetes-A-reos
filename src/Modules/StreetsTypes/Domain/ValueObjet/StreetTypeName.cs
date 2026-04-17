namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

public sealed class StreetTypeName
{
    public string Value { get; }

    public StreetTypeName(string value) => Value = value;

    public static StreetTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 80)
        {
            throw new ArgumentException("Máximo 80 caracteres.");
        }

        return new StreetTypeName(t);
    }
}

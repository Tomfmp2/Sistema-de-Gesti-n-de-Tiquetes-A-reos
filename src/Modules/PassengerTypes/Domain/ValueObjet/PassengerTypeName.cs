namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

public sealed class PassengerTypeName
{
    public string Value { get; }

    public PassengerTypeName(string value) => Value = value;

    public static PassengerTypeName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 50)
        {
            throw new ArgumentException("Máximo 50 caracteres.");
        }

        return new PassengerTypeName(t);
    }
}

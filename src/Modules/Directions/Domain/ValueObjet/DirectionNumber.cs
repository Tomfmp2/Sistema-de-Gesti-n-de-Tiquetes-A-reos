namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

public sealed class DirectionNumber
{
    public string Value { get; }

    public DirectionNumber(string value) => Value = value;

    public static DirectionNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El número no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 20)
        {
            throw new ArgumentException("Máximo 20 caracteres.");
        }

        return new DirectionNumber(t);
    }
}

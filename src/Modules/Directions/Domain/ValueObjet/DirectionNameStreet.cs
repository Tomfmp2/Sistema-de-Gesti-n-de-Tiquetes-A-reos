namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

public sealed class DirectionNameStreet
{
    public string Value { get; }

    public DirectionNameStreet(string value) => Value = value;

    public static DirectionNameStreet Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre de la vía no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 150)
        {
            throw new ArgumentException("Máximo 150 caracteres.");
        }

        return new DirectionNameStreet(t);
    }
}

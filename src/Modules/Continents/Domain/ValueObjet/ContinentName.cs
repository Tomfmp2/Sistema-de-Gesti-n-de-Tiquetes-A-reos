namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

public sealed record ContinentsName
{
    public string Value { get; }

    public ContinentsName(string value)
    {
        Value = value;
    }

    public static ContinentsName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede estar vacío.");
        }

        if (value.Length > 50)
        {
            throw new ArgumentException(
                "El nombre del continente no puede tener más de 50 caracteres."
            );
        }

        return new ContinentsName(value.Trim());
    }

    public override string ToString() => Value;
}

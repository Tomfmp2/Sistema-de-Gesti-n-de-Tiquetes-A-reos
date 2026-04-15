namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

public sealed record ContinentsId
{
    public string Value { get; }

    public ContinentsId(string value)
    {
        Value = value;
    }

    public static ContinentsId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede estar vacío.");
        }

        return new ContinentsId(value.Trim());
    }

    public override string ToString() => Value;
}

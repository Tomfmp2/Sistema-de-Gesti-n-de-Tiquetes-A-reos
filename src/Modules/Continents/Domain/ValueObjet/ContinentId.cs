namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

public sealed record ContinentsId
{
    public int Value { get; }

    public ContinentsId(int value)
    {
        Value = value;
    }

    public static ContinentsId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ContinentsId(value);
    }
}

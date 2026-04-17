namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Domain.ValueObjet;

public sealed class ContinentId
{
    public int Value { get; }

    public ContinentId(int value) => Value = value;

    public static ContinentId Unpersisted => new(0);

    public static ContinentId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "ContinentId debe ser mayor que 0.");
        }

        return new ContinentId(value);
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Cities.Domain.ValueObjet;

public sealed class CityId
{
    public int Value { get; }

    public CityId(int value) => Value = value;

    public static CityId Unpersisted => new(0);

    public static CityId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "CityId debe ser mayor que 0.");
        }

        return new CityId(value);
    }
}

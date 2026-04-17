namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

public sealed class CountryId
{
    public int Value { get; }

    public CountryId(int value) => Value = value;

    public static CountryId Unpersisted => new(0);

    public static CountryId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "CountryId debe ser mayor que 0.");
        }

        return new CountryId(value);
    }
}

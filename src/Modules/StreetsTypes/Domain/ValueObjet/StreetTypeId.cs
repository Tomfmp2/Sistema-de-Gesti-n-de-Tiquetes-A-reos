namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StreetsTypes.Domain.ValueObjet;

public sealed class StreetTypeId
{
    public int Value { get; }

    public StreetTypeId(int value) => Value = value;

    public static StreetTypeId Unpersisted => new(0);

    public static StreetTypeId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new StreetTypeId(value);
    }
}

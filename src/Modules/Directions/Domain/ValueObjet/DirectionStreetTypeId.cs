namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

public sealed class DirectionStreetTypeId
{
    public int Value { get; }

    public DirectionStreetTypeId(int value) => Value = value;

    public static DirectionStreetTypeId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new DirectionStreetTypeId(value);
    }
}

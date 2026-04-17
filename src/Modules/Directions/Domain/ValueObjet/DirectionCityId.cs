namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

public sealed class DirectionCityId
{
    public int Value { get; }

    public DirectionCityId(int value) => Value = value;

    public static DirectionCityId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new DirectionCityId(value);
    }
}

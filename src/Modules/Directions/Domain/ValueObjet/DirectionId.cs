namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

public sealed class DirectionId
{
    public int Value { get; }

    public DirectionId(int value) => Value = value;

    public static DirectionId Unpersisted => new(0);

    public static DirectionId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new DirectionId(value);
    }
}

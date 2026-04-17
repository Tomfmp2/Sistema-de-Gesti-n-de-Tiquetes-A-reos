namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

public sealed class PassengerId
{
    public int Value { get; }

    public PassengerId(int value) => Value = value;

    public static PassengerId Unpersisted => new(0);

    public static PassengerId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PassengerId(value);
    }
}

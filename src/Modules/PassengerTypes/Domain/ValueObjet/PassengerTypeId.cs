namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.PassengerTypes.Domain.ValueObjet;

public sealed class PassengerTypeId
{
    public int Value { get; }

    public PassengerTypeId(int value) => Value = value;

    public static PassengerTypeId Unpersisted => new(0);

    public static PassengerTypeId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PassengerTypeId(value);
    }
}

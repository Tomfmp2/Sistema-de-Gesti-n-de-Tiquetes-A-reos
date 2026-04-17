namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

public sealed class PassengerPersonId
{
    public int Value { get; }

    public PassengerPersonId(int value) => Value = value;

    public static PassengerPersonId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PassengerPersonId(value);
    }
}

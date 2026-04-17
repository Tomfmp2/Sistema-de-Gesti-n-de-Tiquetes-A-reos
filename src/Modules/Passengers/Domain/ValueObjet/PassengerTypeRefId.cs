namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Domain.ValueObjet;

public sealed class PassengerTypeRefId
{
    public int Value { get; }

    public PassengerTypeRefId(int value) => Value = value;

    public static PassengerTypeRefId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new PassengerTypeRefId(value);
    }
}

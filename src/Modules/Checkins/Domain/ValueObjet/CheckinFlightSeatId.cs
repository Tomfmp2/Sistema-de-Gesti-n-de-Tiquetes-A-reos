namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinFlightSeatId
{
    public int Value { get; }

    public CheckinFlightSeatId(int value)
    {
        Value = value;
    }

    public static CheckinFlightSeatId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CheckinFlightSeatId(value);
    }
}

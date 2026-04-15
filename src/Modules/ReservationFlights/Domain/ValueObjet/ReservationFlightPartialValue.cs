namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Domain.ValueObjet;

public class ReservationFlightPartialValue
{
    public decimal Value { get; }

    public ReservationFlightPartialValue(decimal value)
    {
        Value = value;
    }

    public static ReservationFlightPartialValue Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El valor parcial no puede ser menor a 0");
        }

        return new ReservationFlightPartialValue(value);
    }
}

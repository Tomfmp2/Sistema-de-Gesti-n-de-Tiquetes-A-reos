namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

public class ReservationPassengerPassengerId
{
    public int Value { get; }

    public ReservationPassengerPassengerId(int value)
    {
        Value = value;
    }

    public static ReservationPassengerPassengerId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationPassengerPassengerId(value);
    }
}

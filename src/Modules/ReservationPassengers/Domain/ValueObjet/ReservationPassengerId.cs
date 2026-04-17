namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

public class ReservationPassengerId
{
    public int Value { get; }

    public ReservationPassengerId(int value)
    {
        Value = value;
    }

    public static ReservationPassengerId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new ReservationPassengerId(value);
    }
}

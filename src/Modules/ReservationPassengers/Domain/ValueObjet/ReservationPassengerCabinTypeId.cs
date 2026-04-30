namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Domain.ValueObjet;

public class ReservationPassengerCabinTypeId
{
    public int Value { get; }

    private ReservationPassengerCabinTypeId(int value)
    {
        Value = value;
    }

    public static ReservationPassengerCabinTypeId Create(int value)
    {
        if (value < 1)
            throw new ArgumentException("CabinTypeId must be at least 1");
        return new ReservationPassengerCabinTypeId(value);
    }
}

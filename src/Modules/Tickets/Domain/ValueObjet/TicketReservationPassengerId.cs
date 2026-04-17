namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketReservationPassengerId
{
    public int Value { get; }

    public TicketReservationPassengerId(int value)
    {
        Value = value;
    }

    public static TicketReservationPassengerId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new TicketReservationPassengerId(value);
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketId
{
    public int Value { get; }

    public TicketId(int value)
    {
        Value = value;
    }

    public static TicketId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new TicketId(value);
    }
}

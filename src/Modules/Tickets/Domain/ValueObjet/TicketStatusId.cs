namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketStatusId
{
    public int Value { get; }

    public TicketStatusId(int value)
    {
        Value = value;
    }

    public static TicketStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new TicketStatusId(value);
    }
}

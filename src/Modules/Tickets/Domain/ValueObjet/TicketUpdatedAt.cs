namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketUpdatedAt
{
    public DateTime Value { get; }

    public TicketUpdatedAt(DateTime value)
    {
        Value = value;
    }

    public static TicketUpdatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de actualizacion no puede ser vacia");
        }

        return new TicketUpdatedAt(value);
    }
}

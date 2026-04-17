namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketCreatedAt
{
    public DateTime Value { get; }

    public TicketCreatedAt(DateTime value)
    {
        Value = value;
    }

    public static TicketCreatedAt Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de creacion no puede ser vacia");
        }

        return new TicketCreatedAt(value);
    }
}

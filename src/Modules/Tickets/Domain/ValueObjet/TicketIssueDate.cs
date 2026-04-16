namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketIssueDate
{
    public DateTime Value { get; }

    public TicketIssueDate(DateTime value)
    {
        Value = value;
    }

    public static TicketIssueDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de emision no puede ser vacia");
        }

        return new TicketIssueDate(value);
    }
}

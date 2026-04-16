namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Domain.ValueObjet;

public class TicketCode
{
    public string Value { get; }

    public TicketCode(string value)
    {
        Value = value;
    }

    public static TicketCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Length > 30)
        {
            throw new ArgumentException("El valor no puede tener mas de 30 caracteres");
        }

        return new TicketCode(value.Trim());
    }
}

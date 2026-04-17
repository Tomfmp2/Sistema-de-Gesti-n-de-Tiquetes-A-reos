namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.TicketStatuses.Domain.ValueObjet;

public class TicketStatusName
{
    public string Value { get; }

    public TicketStatusName(string value)
    {
        Value = value;
    }

    public static TicketStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new TicketStatusName(value.Trim());
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinTicketId
{
    public int Value { get; }

    public CheckinTicketId(int value)
    {
        Value = value;
    }

    public static CheckinTicketId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CheckinTicketId(value);
    }
}

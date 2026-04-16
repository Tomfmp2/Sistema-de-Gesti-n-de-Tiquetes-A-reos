namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinDate
{
    public DateTime Value { get; }

    public CheckinDate(DateTime value)
    {
        Value = value;
    }

    public static CheckinDate Create(DateTime value)
    {
        if (value == default)
        {
            throw new ArgumentException("La fecha de checkin no puede ser vacia");
        }

        return new CheckinDate(value);
    }
}

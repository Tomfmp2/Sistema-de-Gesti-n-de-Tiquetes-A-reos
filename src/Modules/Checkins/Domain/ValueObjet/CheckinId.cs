namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinId
{
    public int Value { get; }

    public CheckinId(int value)
    {
        Value = value;
    }

    public static CheckinId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CheckinId(value);
    }
}

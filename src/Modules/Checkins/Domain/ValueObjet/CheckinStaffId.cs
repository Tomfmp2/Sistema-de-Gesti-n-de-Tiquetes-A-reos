namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinStaffId
{
    public int Value { get; }

    public CheckinStaffId(int value)
    {
        Value = value;
    }

    public static CheckinStaffId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CheckinStaffId(value);
    }
}

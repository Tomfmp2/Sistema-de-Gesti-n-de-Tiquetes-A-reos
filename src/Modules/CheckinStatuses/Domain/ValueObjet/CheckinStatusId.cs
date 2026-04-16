namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

public class CheckinStatusId
{
    public int Value { get; }

    public CheckinStatusId(int value)
    {
        Value = value;
    }

    public static CheckinStatusId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new CheckinStatusId(value);
    }
}

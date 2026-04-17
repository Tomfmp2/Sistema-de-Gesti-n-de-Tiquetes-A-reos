namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinHasCheckedBaggage
{
    public bool Value { get; }

    public CheckinHasCheckedBaggage(bool value)
    {
        Value = value;
    }

    public static CheckinHasCheckedBaggage Create(bool value)
    {
        return new CheckinHasCheckedBaggage(value);
    }
}

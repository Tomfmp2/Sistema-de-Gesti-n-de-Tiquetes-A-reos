namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareValidFrom
{
    public DateTime? Value { get; }

    public FareValidFrom(DateTime? value)
    {
        Value = value;
    }

    public static FareValidFrom Create(DateTime? value)
    {
        return new FareValidFrom(value);
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareValidTo
{
    public DateTime? Value { get; }

    public FareValidTo(DateTime? value)
    {
        Value = value;
    }

    public static FareValidTo Create(DateTime? value)
    {
        return new FareValidTo(value);
    }
}

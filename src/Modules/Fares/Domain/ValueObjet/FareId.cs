namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareId
{
    public int Value { get; }

    public FareId(int value)
    {
        Value = value;
    }

    public static FareId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FareId(value);
    }
}

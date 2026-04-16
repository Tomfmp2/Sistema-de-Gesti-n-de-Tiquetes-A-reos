namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareSeasonId
{
    public int Value { get; }

    public FareSeasonId(int value)
    {
        Value = value;
    }

    public static FareSeasonId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FareSeasonId(value);
    }
}

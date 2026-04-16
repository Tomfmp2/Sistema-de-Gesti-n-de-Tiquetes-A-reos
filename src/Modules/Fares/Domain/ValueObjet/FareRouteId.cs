namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareRouteId
{
    public int Value { get; }

    public FareRouteId(int value)
    {
        Value = value;
    }

    public static FareRouteId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FareRouteId(value);
    }
}

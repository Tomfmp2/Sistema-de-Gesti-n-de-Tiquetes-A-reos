namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareBasePrice
{
    public decimal Value { get; }

    public FareBasePrice(decimal value)
    {
        Value = value;
    }

    public static FareBasePrice Create(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El precio base no puede ser menor a 0");
        }

        return new FareBasePrice(value);
    }
}

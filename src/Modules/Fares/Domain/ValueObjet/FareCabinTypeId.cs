namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

public class FareCabinTypeId
{
    public int Value { get; }

    public FareCabinTypeId(int value)
    {
        Value = value;
    }

    public static FareCabinTypeId Create(int value)
    {
        if (value <= 0)
        {
            throw new ArgumentException("El valor no puede ser menor a 1");
        }

        return new FareCabinTypeId(value);
    }
}

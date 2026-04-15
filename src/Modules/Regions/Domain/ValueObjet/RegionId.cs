namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

public class RegionId
{
    public int Value { get; }

    public RegionId(int value)
    {
        Value = value;
    }

    public static RegionId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El valor no puede ser menor a 1");

        return new RegionId(value);
    }
}

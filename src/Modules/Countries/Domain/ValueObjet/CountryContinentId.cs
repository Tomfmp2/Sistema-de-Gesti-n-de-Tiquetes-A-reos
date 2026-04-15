namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

public class CountryContinentId
{
    public int Value { get; }

    public CountryContinentId(int value)
    {
        Value = value;
    }

    public static CountryContinentId Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("El valor no puede ser menor a 1");

        return new CountryContinentId(value);
    }
}

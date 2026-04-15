namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

public class RegionCuntryId
{
    public string Value { get; }

    public RegionCuntryId(string value)
    {
        Value = value;
    }

    public static RegionCuntryId Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacío");
        }

        return new RegionCuntryId(value);
    }
}

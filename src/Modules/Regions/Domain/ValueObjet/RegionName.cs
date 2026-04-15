namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

public class RegionName
{
    public string Value { get; }

    public RegionName(string value)
    {
        Value = value;
    }

    public static RegionName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacío");
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("El valor no puede tener más de 100 caracteres");
        }

        return new RegionName(value.Trim());
    }
}

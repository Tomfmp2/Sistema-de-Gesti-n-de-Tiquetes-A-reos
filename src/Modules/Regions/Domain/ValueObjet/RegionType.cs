namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

public class RegionType
{
    public string Value { get; }

    public RegionType(string value)
    {
        Value = value;
    }

    public static RegionType Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacío");
        }

        if (value.Length > 30)
        {
            throw new ArgumentException("El valor no puede tener más de 30 caracteres");
        }

        return new RegionType(value.Trim());
    }
}

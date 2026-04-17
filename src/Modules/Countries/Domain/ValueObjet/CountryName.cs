namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Domain.ValueObjet;

public sealed record CountryName
{
    public string Value { get; }

    public CountryName(string value)
    {
        Value = value;
    }

    public static CountryName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacío");
        }

        if (value.Length > 100)
        {
            throw new ArgumentException("El valor no puede tener más de 100 caracteres");
        }

        return new CountryName(value);
    }
}

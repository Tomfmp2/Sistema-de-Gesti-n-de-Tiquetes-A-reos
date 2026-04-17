namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObjet;

public class FlightStatusName
{
    public string Value { get; }

    public FlightStatusName(string value)
    {
        Value = value;
    }

    public static FlightStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Trim().Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new FlightStatusName(value.Trim());
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Domain.ValueObjet;

public class FlightCode
{
    public string Value { get; }

    public FlightCode(string value)
    {
        Value = value;
    }

    public static FlightCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Length > 10)
        {
            throw new ArgumentException("El valor no puede tener mas de 10 caracteres");
        }

        return new FlightCode(value.Trim());
    }
}

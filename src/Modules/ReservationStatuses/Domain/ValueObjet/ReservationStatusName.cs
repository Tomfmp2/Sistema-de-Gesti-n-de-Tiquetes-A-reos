namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

public class ReservationStatusName
{
    public string Value { get; }

    public ReservationStatusName(string value)
    {
        Value = value;
    }

    public static ReservationStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new ReservationStatusName(value.Trim());
    }
}

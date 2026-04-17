namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CheckinStatuses.Domain.ValueObjet;

public class CheckinStatusName
{
    public string Value { get; }

    public CheckinStatusName(string value)
    {
        Value = value;
    }

    public static CheckinStatusName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Length > 50)
        {
            throw new ArgumentException("El valor no puede tener mas de 50 caracteres");
        }

        return new CheckinStatusName(value.Trim());
    }
}

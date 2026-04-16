namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinBoardingPassNumber
{
    public string Value { get; }

    public CheckinBoardingPassNumber(string value)
    {
        Value = value;
    }

    public static CheckinBoardingPassNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El valor no puede ser nulo ni vacio");
        }

        if (value.Length > 20)
        {
            throw new ArgumentException("El valor no puede tener mas de 20 caracteres");
        }

        return new CheckinBoardingPassNumber(value.Trim());
    }
}

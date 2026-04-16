namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

public class CheckinBaggageWeightKg
{
    public decimal? Value { get; }

    public CheckinBaggageWeightKg(decimal? value)
    {
        Value = value;
    }

    public static CheckinBaggageWeightKg Create(decimal? value)
    {
        if (value < 0)
        {
            throw new ArgumentException("El peso del equipaje no puede ser menor a 0");
        }

        return new CheckinBaggageWeightKg(value);
    }
}

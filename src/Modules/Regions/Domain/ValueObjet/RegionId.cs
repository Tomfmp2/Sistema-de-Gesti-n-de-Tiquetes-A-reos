namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

public sealed class RegionId
{
    public int Value { get; }

    public RegionId(int value) => Value = value;

    public static RegionId Unpersisted => new(0);

    public static RegionId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value), "RegionId debe ser mayor que 0.");
        }

        return new RegionId(value);
    }
}

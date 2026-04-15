namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Regions.Domain.ValueObjet;

public sealed class RegionCuntryId
{
    public int Value { get; }

    public RegionCuntryId(int value) => Value = value;

    public static RegionCuntryId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentException("El país (región) debe ser un id válido.");
        }

        return new RegionCuntryId(value);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Aggregate;

public class AircraftManufacturer
{
    public AircraftManufacturerId Id { get; private set; }
    public AircraftManufacturerName Name { get; private set; }
    public Country Country { get; private set; }

    private AircraftManufacturer(
        AircraftManufacturerId id,
        AircraftManufacturerName name,
        Country country)
    {
        Id = id;
        Name = name;
        Country = country;
    }

    public static AircraftManufacturer Create(
        AircraftManufacturerId id,
        AircraftManufacturerName name,
        Country country)
    {
        return new AircraftManufacturer(id, name, country);
    }

    public static AircraftManufacturer Reconstitute(
        AircraftManufacturerId id,
        AircraftManufacturerName name,
        Country country)
    {
        return new AircraftManufacturer(id, name, country);
    }
}
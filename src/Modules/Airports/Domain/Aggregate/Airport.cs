using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Domain.Aggregate;

public sealed class Airport
{
    public AirportId Id { get; internal set; }
    public AirportName Name { get; private set; }
    public IataCode IataCode { get; private set; }
    public IcaoCode IcaoCode { get; private set; }
    public int CityId { get; private set; }

    private Airport(AirportId id, AirportName name, IataCode iataCode, IcaoCode icaoCode, int cityId)
    {
        Id = id;
        Name = name;
        IataCode = iataCode;
        IcaoCode = icaoCode;
        CityId = cityId;
    }

    public static Airport Create(AirportName name, IataCode iataCode, IcaoCode icaoCode, int cityId)
    {
        return new Airport(
            AirportId.Reconstitute(0), // will be set by DB
            name,
            iataCode,
            icaoCode,
            cityId
        );
    }

    public static Airport Reconstitute(AirportId id, AirportName name, IataCode iataCode, IcaoCode icaoCode, int cityId)
    {
        return new Airport(id, name, iataCode, icaoCode, cityId);
    }

    public void Update(AirportName name, IataCode iataCode, IcaoCode icaoCode, int cityId)
    {
        Name = name;
        IataCode = iataCode;
        IcaoCode = icaoCode;
        CityId = cityId;
    }
}
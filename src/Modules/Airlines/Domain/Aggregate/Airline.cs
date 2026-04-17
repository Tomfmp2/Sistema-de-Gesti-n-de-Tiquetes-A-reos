using System;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;

public sealed class Airline
{
    public AirlineId Id { get; internal set; }
    public AirlineName Name { get; private set; }
    public IataCode IataCode { get; private set; }
    public int OriginCountryId { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Airline(AirlineId id, AirlineName name, IataCode iataCode, int originCountryId, bool isActive, DateTime createdAt, DateTime updatedAt)
    {
        Id = id;
        Name = name;
        IataCode = iataCode;
        OriginCountryId = originCountryId;
        IsActive = isActive;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Airline Create(AirlineName name, IataCode iataCode, int originCountryId)
    {
        return new Airline(
            AirlineId.Create(0), // will be set by DB
            name,
            iataCode,
            originCountryId,
            true,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
    }

    public static Airline Reconstitute(AirlineId id, AirlineName name, IataCode iataCode, int originCountryId, bool isActive, DateTime createdAt, DateTime updatedAt)
    {
        return new Airline(id, name, iataCode, originCountryId, isActive, createdAt, updatedAt);
    }

    public void Update(AirlineName name, IataCode iataCode, int originCountryId, bool isActive)
    {
        Name = name;
        IataCode = iataCode;
        OriginCountryId = originCountryId;
        IsActive = isActive;
        UpdatedAt = DateTime.UtcNow;
    }
}
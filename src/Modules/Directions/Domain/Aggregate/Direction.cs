using sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Directions.Domain.Aggregate;

public sealed class Direction
{
    public DirectionId Id { get; private set; }
    public DirectionCityId CityId { get; private set; }
    public DirectionStreetTypeId StreetTypeId { get; private set; }
    public DirectionNameStreet StreetName { get; private set; }
    public DirectionNumber Number { get; private set; }
    public string? Complement { get; private set; }
    public string? PostalCode { get; private set; }

    private Direction(
        DirectionId id,
        DirectionCityId cityId,
        DirectionStreetTypeId streetTypeId,
        DirectionNameStreet streetName,
        DirectionNumber number,
        string? complement,
        string? postalCode
    )
    {
        Id = id;
        CityId = cityId;
        StreetTypeId = streetTypeId;
        StreetName = streetName;
        Number = number;
        Complement = complement;
        PostalCode = postalCode;
    }

    private static string? NormOptional(string? v, int max)
    {
        if (string.IsNullOrWhiteSpace(v))
        {
            return null;
        }

        var t = v.Trim();
        if (t.Length > max)
        {
            throw new ArgumentException($"Máximo {max} caracteres.");
        }

        return t;
    }

    public static Direction CreateNew(
        DirectionCityId cityId,
        DirectionStreetTypeId streetTypeId,
        DirectionNameStreet streetName,
        DirectionNumber number,
        string? complement,
        string? postalCode
    )
    {
        return new Direction(
            DirectionId.Unpersisted,
            cityId,
            streetTypeId,
            streetName,
            number,
            NormOptional(complement, 100),
            NormOptional(postalCode, 20)
        );
    }

    public static Direction Create(
        DirectionId id,
        DirectionCityId cityId,
        DirectionStreetTypeId streetTypeId,
        DirectionNameStreet streetName,
        DirectionNumber number,
        string? complement,
        string? postalCode
    )
    {
        return new Direction(
            id,
            cityId,
            streetTypeId,
            streetName,
            number,
            NormOptional(complement, 100),
            NormOptional(postalCode, 20)
        );
    }
}

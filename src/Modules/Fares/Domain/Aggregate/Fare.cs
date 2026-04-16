using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Domain.Aggregate;

public class Fare
{
    public FareId Id { get; private set; }
    public FareRouteId RouteId { get; private set; }
    public FareCabinTypeId CabinTypeId { get; private set; }
    public FarePassengerTypeId PassengerTypeId { get; private set; }
    public FareSeasonId SeasonId { get; private set; }
    public FareBasePrice BasePrice { get; private set; }
    public FareValidFrom ValidFrom { get; private set; }
    public FareValidTo ValidTo { get; private set; }

    private Fare(
        FareId id,
        FareRouteId routeId,
        FareCabinTypeId cabinTypeId,
        FarePassengerTypeId passengerTypeId,
        FareSeasonId seasonId,
        FareBasePrice basePrice,
        FareValidFrom validFrom,
        FareValidTo validTo
    )
    {
        Id = id;
        RouteId = routeId;
        CabinTypeId = cabinTypeId;
        PassengerTypeId = passengerTypeId;
        SeasonId = seasonId;
        BasePrice = basePrice;
        ValidFrom = validFrom;
        ValidTo = validTo;
    }

    public static Fare Create(
        FareId id,
        FareRouteId routeId,
        FareCabinTypeId cabinTypeId,
        FarePassengerTypeId passengerTypeId,
        FareSeasonId seasonId,
        FareBasePrice basePrice,
        FareValidFrom validFrom,
        FareValidTo validTo
    )
    {
        return new Fare(
            id,
            routeId,
            cabinTypeId,
            passengerTypeId,
            seasonId,
            basePrice,
            validFrom,
            validTo
        );
    }
}

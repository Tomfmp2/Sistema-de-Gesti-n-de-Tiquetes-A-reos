using sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Fares.Infrastructure.Data;

public static class FareDefaultData
{
    private static readonly DateTime ValidFrom = new(2026, 1, 1);

    public static readonly FareEntity[] Fares =
    [
        new() { Id = 1, RouteId = 1, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 180000.00m, ValidFrom = ValidFrom },
        new() { Id = 2, RouteId = 2, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 210000.00m, ValidFrom = ValidFrom },
        new() { Id = 3, RouteId = 3, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 260000.00m, ValidFrom = ValidFrom },
        new() { Id = 4, RouteId = 4, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 950000.00m, ValidFrom = ValidFrom },
        new() { Id = 5, RouteId = 5, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 1100000.00m, ValidFrom = ValidFrom },
        new() { Id = 6, RouteId = 6, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 2800000.00m, ValidFrom = ValidFrom },
        new() { Id = 7, RouteId = 7, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 780000.00m, ValidFrom = ValidFrom },
        new() { Id = 8, RouteId = 8, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 720000.00m, ValidFrom = ValidFrom },
        new() { Id = 9, RouteId = 9, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 2200000.00m, ValidFrom = ValidFrom },
        new() { Id = 10, RouteId = 10, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 420000.00m, ValidFrom = ValidFrom },
        new() { Id = 11, RouteId = 11, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 480000.00m, ValidFrom = ValidFrom },
        new() { Id = 12, RouteId = 12, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 690000.00m, ValidFrom = ValidFrom },
        new() { Id = 13, RouteId = 13, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 510000.00m, ValidFrom = ValidFrom },
        new() { Id = 14, RouteId = 14, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 850000.00m, ValidFrom = ValidFrom },
        new() { Id = 15, RouteId = 15, CabinTypeId = 1, PassengerTypeId = 3, SeasonId = 2, BasePrice = 3100000.00m, ValidFrom = ValidFrom },

        // Ejecutiva (x2.2)
        new() { Id = 16, RouteId = 1, CabinTypeId = 3, PassengerTypeId = 3, SeasonId = 2, BasePrice = 396000.00m, ValidFrom = ValidFrom },
        new() { Id = 17, RouteId = 2, CabinTypeId = 3, PassengerTypeId = 3, SeasonId = 2, BasePrice = 462000.00m, ValidFrom = ValidFrom },
        new() { Id = 18, RouteId = 4, CabinTypeId = 3, PassengerTypeId = 3, SeasonId = 2, BasePrice = 2090000.00m, ValidFrom = ValidFrom },
        new() { Id = 19, RouteId = 14, CabinTypeId = 3, PassengerTypeId = 3, SeasonId = 2, BasePrice = 1870000.00m, ValidFrom = ValidFrom },
        new() { Id = 20, RouteId = 11, CabinTypeId = 3, PassengerTypeId = 3, SeasonId = 2, BasePrice = 1056000.00m, ValidFrom = ValidFrom },

        // Primera Clase (x3.8)
        new() { Id = 21, RouteId = 1, CabinTypeId = 4, PassengerTypeId = 3, SeasonId = 2, BasePrice = 684000.00m, ValidFrom = ValidFrom },
        new() { Id = 22, RouteId = 2, CabinTypeId = 4, PassengerTypeId = 3, SeasonId = 2, BasePrice = 798000.00m, ValidFrom = ValidFrom },
        new() { Id = 23, RouteId = 4, CabinTypeId = 4, PassengerTypeId = 3, SeasonId = 2, BasePrice = 3610000.00m, ValidFrom = ValidFrom },
        new() { Id = 24, RouteId = 14, CabinTypeId = 4, PassengerTypeId = 3, SeasonId = 2, BasePrice = 3230000.00m, ValidFrom = ValidFrom },
        new() { Id = 25, RouteId = 11, CabinTypeId = 4, PassengerTypeId = 3, SeasonId = 2, BasePrice = 1824000.00m, ValidFrom = ValidFrom }
    ];
}

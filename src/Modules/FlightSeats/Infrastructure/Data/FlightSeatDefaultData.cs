using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Data;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Data;

public static class FlightSeatDefaultData
{
    public static readonly FlightSeatEntity[] FlightSeats = Build();

    private static FlightSeatEntity[] Build()
    {
        var nextId = 1;
        return FlightSeatLayoutGenerator
            .BuildAllSeats(
                FlightDefaultData.Flights,
                CabinConfigurationDefaultData.CabinConfigurations,
                ref nextId
            )
            .ToArray();
    }

    public static int CountForFlight(int flightId) => FlightSeats.Count(s => s.FlightId == flightId);
}

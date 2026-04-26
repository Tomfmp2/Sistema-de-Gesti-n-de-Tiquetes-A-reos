using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Data;

public static class FlightSeatDefaultData
{
    public static readonly FlightSeatEntity[] FlightSeats = CreateFlightSeats();

    private static FlightSeatEntity[] CreateFlightSeats()
    {
        var seats = new List<FlightSeatEntity>();
        var id = 1;

        for (var flightId = 1; flightId <= 5; flightId++)
        {
            foreach (var seat in new[] { "1A", "1B", "1C", "1D", "1E", "1F" })
            {
                seats.Add(new FlightSeatEntity
                {
                    Id = id++,
                    FlightId = flightId,
                    SeatCode = seat,
                    CabinTypeId = 1,
                    LocationTypeId = GetLocationTypeId(seat[^1]),
                    Status = "Disponible"
                });
            }
        }

        return [.. seats];
    }

    private static int GetLocationTypeId(char seatLetter)
    {
        return seatLetter switch
        {
            'A' or 'F' => 1,
            'C' or 'D' => 2,
            _ => 3
        };
    }
}

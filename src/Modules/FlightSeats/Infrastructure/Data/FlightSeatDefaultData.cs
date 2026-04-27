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
            for (var row = 1; row <= 15; row++)
            {
                foreach (var col in new[] { "A", "B", "C", "D", "E", "F" })
                {
                    var seatCode = $"{row}{col}";
                    var cabinTypeId = row switch
                    {
                        <= 2 => 4, // Primera Clase
                        <= 5 => 3, // Ejecutiva
                        _ => 1     // Económica
                    };

                    seats.Add(new FlightSeatEntity
                    {
                        Id = id++,
                        FlightId = flightId,
                        SeatCode = seatCode,
                        CabinTypeId = cabinTypeId,
                        LocationTypeId = GetLocationTypeId(col[0]),
                        Status = "Disponible"
                    });
                }
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

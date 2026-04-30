using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Data;

/// <summary>
/// Genera asientos de vuelo a partir de la configuración de cabinas del avión asignado al vuelo.
/// </summary>
public static class FlightSeatLayoutGenerator
{
    public static IReadOnlyList<FlightSeatEntity> BuildAllSeats(
        IReadOnlyList<FlightEntity> flights,
        IReadOnlyList<CabinConfigurationEntity> cabinConfigurations,
        ref int nextId
    )
    {
        var list = new List<FlightSeatEntity>();
        foreach (var flight in flights)
        {
            var configs = cabinConfigurations
                .Where(c => c.AircraftId == flight.AircraftId)
                .OrderBy(c => c.CabinTypeId)
                .ThenBy(c => c.StartRow)
                .ToList();
            foreach (var cfg in configs)
            {
                var letters = cfg.SeatLetters.Trim();
                if (string.IsNullOrEmpty(letters))
                    continue;
                var letterCount = Math.Min(cfg.SeatsPerRow, letters.Length);
                for (var row = cfg.StartRow; row <= cfg.EndRow; row++)
                {
                    for (var i = 0; i < letterCount; i++)
                    {
                        var ch = letters[i];
                        var code = $"{row}{ch}";
                        if (code.Length > 5)
                            code = code[^5..]; // varchar(5) en BD
                        list.Add(
                            new FlightSeatEntity
                            {
                                Id = nextId++,
                                FlightId = flight.Id,
                                SeatCode = code,
                                CabinTypeId = cfg.CabinTypeId,
                                LocationTypeId = GetLocationTypeId(ch),
                                IsOccupied = false
                            }
                        );
                    }
                }
            }
        }

        return list;
    }

    private static int GetLocationTypeId(char seatLetter)
    {
        // 1 Ventana, 2 Pasillo, 3 Centro — alineado con CreateSeatLocationTypes
        return seatLetter switch
        {
            'A' or 'F' or 'H' => 1,
            'C' or 'D' => 2,
            _ => 3
        };
    }
}

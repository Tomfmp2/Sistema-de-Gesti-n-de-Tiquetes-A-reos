using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Infrastructure.Data;

public static class AirportAirlineDefaultData
{
    public static readonly AirportAirlineEntity[] AirportAirlines =
    [
        new() { Id = 1, AirportId = 1, AirlineId = 1, Terminal = "1", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 2, AirportId = 2, AirlineId = 1, Terminal = "Nacional", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 3, AirportId = 3, AirlineId = 1, Terminal = "Principal", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 4, AirportId = 4, AirlineId = 1, Terminal = "Principal", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 5, AirportId = 5, AirlineId = 1, Terminal = "S", StartDate = new DateOnly(2021, 1, 1), IsActive = true },
        new() { Id = 6, AirportId = 5, AirlineId = 3, Terminal = "D", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 7, AirportId = 6, AirlineId = 3, Terminal = "8", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 8, AirportId = 7, AirlineId = 3, Terminal = "4", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 9, AirportId = 9, AirlineId = 4, Terminal = "4", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 10, AirportId = 10, AirlineId = 5, Terminal = "2E", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 11, AirportId = 14, AirlineId = 2, Terminal = "2", StartDate = new DateOnly(2020, 1, 1), IsActive = true },
        new() { Id = 12, AirportId = 15, AirlineId = 2, Terminal = "Principal", StartDate = new DateOnly(2020, 1, 1), IsActive = true }
    ];
}

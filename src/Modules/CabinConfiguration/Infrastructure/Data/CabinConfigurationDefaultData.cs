using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Data;

public static class CabinConfigurationDefaultData
{
    public static readonly CabinConfigurationEntity[] CabinConfigurations =
    [
        new() { Id = 1, AircraftId = 1, CabinTypeId = 1, StartRow = 1, EndRow = 30, SeatsPerRow = 6, SeatLetters = "ABCDEF" },
        new() { Id = 2, AircraftId = 2, CabinTypeId = 1, StartRow = 1, EndRow = 30, SeatsPerRow = 6, SeatLetters = "ABCDEF" },
        new() { Id = 3, AircraftId = 3, CabinTypeId = 1, StartRow = 1, EndRow = 31, SeatsPerRow = 6, SeatLetters = "ABCDEF" },
        new() { Id = 4, AircraftId = 4, CabinTypeId = 3, StartRow = 1, EndRow = 6, SeatsPerRow = 4, SeatLetters = "ACDF" },
        new() { Id = 5, AircraftId = 4, CabinTypeId = 1, StartRow = 7, EndRow = 34, SeatsPerRow = 6, SeatLetters = "ABCDEF" },
        new() { Id = 6, AircraftId = 5, CabinTypeId = 3, StartRow = 1, EndRow = 5, SeatsPerRow = 4, SeatLetters = "ACDF" },
        new() { Id = 7, AircraftId = 5, CabinTypeId = 1, StartRow = 6, EndRow = 42, SeatsPerRow = 8, SeatLetters = "ABCDEFGH" },
        new() { Id = 8, AircraftId = 6, CabinTypeId = 3, StartRow = 1, EndRow = 5, SeatsPerRow = 4, SeatLetters = "ACDF" },
        new() { Id = 9, AircraftId = 6, CabinTypeId = 1, StartRow = 6, EndRow = 42, SeatsPerRow = 8, SeatLetters = "ABCDEFGH" }
    ];
}

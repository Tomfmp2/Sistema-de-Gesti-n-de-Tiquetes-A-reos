using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases;

public sealed class GenerateFlightSeatsUseCase
{
    private readonly IFlightSeatRepository _flightSeatRepository;
    private readonly ICabinConfigurationRepository _cabinConfigurationRepository;

    public GenerateFlightSeatsUseCase(
        IFlightSeatRepository flightSeatRepository,
        ICabinConfigurationRepository cabinConfigurationRepository)
    {
        _flightSeatRepository = flightSeatRepository ?? throw new ArgumentNullException(nameof(flightSeatRepository));
        _cabinConfigurationRepository = cabinConfigurationRepository ?? throw new ArgumentNullException(nameof(cabinConfigurationRepository));
    }

    public async Task<int> ExecuteAsync(int flightId, int aircraftId, CancellationToken cancellationToken = default)
    {
        var configurations = await _cabinConfigurationRepository.GetAllAsync();
        var aircraftConfigs = configurations.Where(c => c.AircraftId.Value == aircraftId).ToList();

        if (!aircraftConfigs.Any())
            return 0;

        int count = 0;
        foreach (var config in aircraftConfigs)
        {
            var letters = config.SeatLetters.Value;
            if (string.IsNullOrEmpty(letters))
                continue;

            for (int row = config.StartRow.Value; row <= config.EndRow.Value; row++)
            {
                foreach (char letter in letters)
                {
                    var seatCode = $"{row}{letter}";
                    var locationTypeId = GetLocationTypeId(letter, letters);
                    var status = SeatStatus.Create("Disponible");

                    var flightSeat = FlightSeat.Create(
                        new FlightId(flightId),
                        new SeatCode(seatCode),
                        new CabinTypeId(config.CabinTypeId.Value),
                        new LocationTypeId(locationTypeId),
                        status);

                    await _flightSeatRepository.AddAsync(flightSeat, cancellationToken);
                    count++;
                }
            }
        }

        return count;
    }

    private static int GetLocationTypeId(char seatLetter, string allLetters)
    {
        // Simplification for typical configurations
        // A and F usually window (1)
        // C and D usually aisle (2)
        // Others middle (3)
        // We can make it slightly dynamic: first and last letters are Window.
        if (allLetters.Length > 0)
        {
            if (seatLetter == allLetters[0] || seatLetter == allLetters[^1])
                return 1; // Window
        }

        if (seatLetter == 'C' || seatLetter == 'D')
            return 2; // Aisle

        return 3; // Middle
    }
}

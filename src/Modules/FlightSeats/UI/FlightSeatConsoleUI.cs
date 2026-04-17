using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.Application.UseCases;

namespace sistema_gestor_de_tiquetes_aereos.src.Modules.FlightSeats.UI;

public sealed class FlightSeatConsoleUI
{
    private readonly IFlightSeatRepository _repository;

    public FlightSeatConsoleUI(IFlightSeatRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Flight Seat Management ===");
            Console.WriteLine("1. Create Flight Seat");
            Console.WriteLine("2. Get Flight Seat by ID");
            Console.WriteLine("3. Get All Flight Seats");
            Console.WriteLine("4. Update Flight Seat");
            Console.WriteLine("5. Delete Flight Seat");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateFlightSeatAsync();
                    break;
                case "2":
                    await GetFlightSeatByIdAsync();
                    break;
                case "3":
                    await GetAllFlightSeatsAsync();
                    break;
                case "4":
                    await UpdateFlightSeatAsync();
                    break;
                case "5":
                    await DeleteFlightSeatAsync();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateFlightSeatAsync()
    {
        Console.Write("Enter Flight ID: ");
        if (!int.TryParse(Console.ReadLine(), out int flightId) || flightId <= 0)
        {
            Console.WriteLine("Invalid Flight ID.");
            return;
        }

        Console.Write("Enter Seat Code (max 5 characters): ");
        string? seatCode = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(seatCode) || seatCode.Length > 5)
        {
            Console.WriteLine("Invalid Seat Code.");
            return;
        }

        Console.Write("Enter Cabin Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int cabinTypeId) || cabinTypeId <= 0)
        {
            Console.WriteLine("Invalid Cabin Type ID.");
            return;
        }

        Console.Write("Enter Location Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int locationTypeId) || locationTypeId <= 0)
        {
            Console.WriteLine("Invalid Location Type ID.");
            return;
        }

        Console.Write("Is seat occupied? (y/n): ");
        bool isOccupied = Console.ReadLine()?.ToLower() == "y";

        try
        {
            var useCase = new CreateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(flightId, seatCode, cabinTypeId, locationTypeId, isOccupied);
            Console.WriteLine($"Flight Seat created successfully with ID: {flightSeat.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetFlightSeatByIdAsync()
    {
        Console.Write("Enter Flight Seat ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        try
        {
            var useCase = new GetFlightSeatByIdUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id);
            
            if (flightSeat == null)
            {
                Console.WriteLine("Flight Seat not found.");
                return;
            }

            DisplayFlightSeat(flightSeat);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetAllFlightSeatsAsync()
    {
        try
        {
            var useCase = new GetAllFlightSeatsUseCase(_repository);
            var flightSeats = await useCase.ExecuteAsync();
            
            var list = flightSeats.ToList();
            if (!list.Any())
            {
                Console.WriteLine("No flight seats found.");
                return;
            }

            Console.WriteLine("\n=== Flight Seats ===");
            foreach (var fs in list)
            {
                Console.WriteLine($"ID: {fs.Id} | Flight: {fs.FlightId} | Seat: {fs.SeatCode} | Cabin: {fs.CabinTypeId} | Location: {fs.LocationTypeId} | Occupied: {fs.IsOccupied}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task UpdateFlightSeatAsync()
    {
        Console.Write("Enter Flight Seat ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        Console.Write("Enter new Flight ID (or press Enter to skip): ");
        int? newFlightId = null;
        string? flightInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(flightInput) && int.TryParse(flightInput, out int fid) && fid > 0)
            newFlightId = fid;

        Console.Write("Enter new Seat Code (or press Enter to skip): ");
        string? newSeatCode = Console.ReadLine();
        if (string.IsNullOrEmpty(newSeatCode))
            newSeatCode = null;

        Console.Write("Enter new Cabin Type ID (or press Enter to skip): ");
        int? newCabinTypeId = null;
        string? cabinInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(cabinInput) && int.TryParse(cabinInput, out int cid) && cid > 0)
            newCabinTypeId = cid;

        Console.Write("Enter new Location Type ID (or press Enter to skip): ");
        int? newLocationTypeId = null;
        string? locationInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(locationInput) && int.TryParse(locationInput, out int lid) && lid > 0)
            newLocationTypeId = lid;

        Console.Write("Update occupancy status? (y/n or press Enter to skip): ");
        bool? newIsOccupied = null;
        string? occupiedInput = Console.ReadLine();
        if (occupiedInput?.ToLower() == "y")
            newIsOccupied = true;
        else if (occupiedInput?.ToLower() == "n")
            newIsOccupied = false;

        try
        {
            var useCase = new UpdateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id, newFlightId, newSeatCode, newCabinTypeId, newLocationTypeId, newIsOccupied);
            
            if (flightSeat == null)
            {
                Console.WriteLine("Flight Seat not found.");
                return;
            }

            Console.WriteLine("Flight Seat updated successfully.");
            DisplayFlightSeat(flightSeat);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteFlightSeatAsync()
    {
        Console.Write("Enter Flight Seat ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        try
        {
            var useCase = new DeleteFlightSeatUseCase(_repository);
            bool deleted = await useCase.ExecuteAsync(id);
            
            if (!deleted)
            {
                Console.WriteLine("Flight Seat not found.");
                return;
            }

            Console.WriteLine("Flight Seat deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void DisplayFlightSeat(Domain.Aggregate.FlightSeat flightSeat)
    {
        Console.WriteLine($"\nID: {flightSeat.Id}");
        Console.WriteLine($"Flight ID: {flightSeat.FlightId}");
        Console.WriteLine($"Seat Code: {flightSeat.SeatCode}");
        Console.WriteLine($"Cabin Type ID: {flightSeat.CabinTypeId}");
        Console.WriteLine($"Location Type ID: {flightSeat.LocationTypeId}");
        Console.WriteLine($"Is Occupied: {flightSeat.IsOccupied}");
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Application.UseCases;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.UI;

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
            Console.WriteLine("\n=== Asientos por vuelo ===");
            Console.WriteLine("1. Crear asiento de vuelo");
            Console.WriteLine("2. Consultar asiento por ID");
            Console.WriteLine("3. Listar todos los asientos");
            Console.WriteLine("4. Actualizar asiento");
            Console.WriteLine("5. Eliminar asiento");
            Console.WriteLine("6. Salir");
            Console.Write("Elija una opción: ");

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
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private async Task CreateFlightSeatAsync()
    {
        Console.Write("ID vuelo: ");
        if (!int.TryParse(Console.ReadLine(), out int flightId) || flightId <= 0)
        {
            Console.WriteLine("ID de vuelo no válido.");
            return;
        }

        Console.Write("Código de asiento (máx. 5 caracteres): ");
        string? seatCode = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(seatCode) || seatCode.Length > 5)
        {
            Console.WriteLine("Código de asiento no válido.");
            return;
        }

        Console.Write("ID tipo de cabina: ");
        if (!int.TryParse(Console.ReadLine(), out int cabinTypeId) || cabinTypeId <= 0)
        {
            Console.WriteLine("ID de tipo de cabina no válido.");
            return;
        }

        Console.Write("ID tipo de ubicación: ");
        if (!int.TryParse(Console.ReadLine(), out int locationTypeId) || locationTypeId <= 0)
        {
            Console.WriteLine("ID de tipo de ubicación no válido.");
            return;
        }

        Console.Write("¿Asiento ocupado? (s/n): ");
        var occ = Console.ReadLine()?.Trim().ToLowerInvariant();
        bool isOccupied = occ is "y" or "s";

        try
        {
            var useCase = new CreateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(flightId, seatCode, cabinTypeId, locationTypeId, isOccupied);
            Console.WriteLine($"Asiento creado correctamente (ID: {flightSeat.Id})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetFlightSeatByIdAsync()
    {
        Console.Write("ID asiento de vuelo: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        try
        {
            var useCase = new GetFlightSeatByIdUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id);
            
            if (flightSeat == null)
            {
                Console.WriteLine("Asiento no encontrado.");
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
                Console.WriteLine("No hay asientos de vuelo registrados.");
                return;
            }

            Console.WriteLine("\n=== Asientos ===");
            foreach (var fs in list)
            {
                Console.WriteLine($"ID: {fs.Id} | Vuelo: {fs.FlightId} | Asiento: {fs.SeatCode} | Cabina: {fs.CabinTypeId} | Ubicación: {fs.LocationTypeId} | Ocupado: {fs.IsOccupied}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task UpdateFlightSeatAsync()
    {
        Console.Write("ID asiento de vuelo: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        Console.Write("Nuevo ID vuelo (Enter para omitir): ");
        int? newFlightId = null;
        string? flightInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(flightInput) && int.TryParse(flightInput, out int fid) && fid > 0)
            newFlightId = fid;

        Console.Write("Nuevo código de asiento (Enter para omitir): ");
        string? newSeatCode = Console.ReadLine();
        if (string.IsNullOrEmpty(newSeatCode))
            newSeatCode = null;

        Console.Write("Nuevo ID tipo de cabina (Enter para omitir): ");
        int? newCabinTypeId = null;
        string? cabinInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(cabinInput) && int.TryParse(cabinInput, out int cid) && cid > 0)
            newCabinTypeId = cid;

        Console.Write("Nuevo ID tipo de ubicación (Enter para omitir): ");
        int? newLocationTypeId = null;
        string? locationInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(locationInput) && int.TryParse(locationInput, out int lid) && lid > 0)
            newLocationTypeId = lid;

        Console.Write("¿Actualizar ocupación? (s/n o Enter para omitir): ");
        bool? newIsOccupied = null;
        string? occupiedInput = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (occupiedInput is "y" or "s")
            newIsOccupied = true;
        else if (occupiedInput is "n")
            newIsOccupied = false;

        try
        {
            var useCase = new UpdateFlightSeatUseCase(_repository);
            var flightSeat = await useCase.ExecuteAsync(id, newFlightId, newSeatCode, newCabinTypeId, newLocationTypeId, newIsOccupied);
            
            if (flightSeat == null)
            {
                Console.WriteLine("Asiento no encontrado.");
                return;
            }

            Console.WriteLine("Asiento actualizado correctamente.");
            DisplayFlightSeat(flightSeat);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteFlightSeatAsync()
    {
        Console.Write("ID asiento a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        try
        {
            var useCase = new DeleteFlightSeatUseCase(_repository);
            bool deleted = await useCase.ExecuteAsync(id);
            
            if (!deleted)
            {
                Console.WriteLine("Asiento no encontrado.");
                return;
            }

            Console.WriteLine("Asiento eliminado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private static void DisplayFlightSeat(Domain.Aggregate.FlightSeat flightSeat)
    {
        Console.WriteLine($"\nID: {flightSeat.Id}");
        Console.WriteLine($"ID vuelo: {flightSeat.FlightId}");
        Console.WriteLine($"Código asiento: {flightSeat.SeatCode}");
        Console.WriteLine($"ID tipo cabina: {flightSeat.CabinTypeId}");
        Console.WriteLine($"ID tipo ubicación: {flightSeat.LocationTypeId}");
        Console.WriteLine($"Ocupado: {flightSeat.IsOccupied}");
    }
}

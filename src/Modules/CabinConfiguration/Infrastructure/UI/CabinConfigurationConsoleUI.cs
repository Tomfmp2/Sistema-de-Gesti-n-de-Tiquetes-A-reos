using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.UI;

public class CabinConfigurationConsoleUI : IModuleUI
{
    private readonly CreateCabinConfigurationUseCase _createUseCase;
    private readonly GetAllCabinConfigurationsUseCase _getAllUseCase;
    private readonly GetCabinConfigurationByIdUseCase _getByIdUseCase;
    private readonly UpdateCabinConfigurationUseCase _updateUseCase;
    private readonly DeleteCabinConfigurationUseCase _deleteUseCase;

    public CabinConfigurationConsoleUI(AppDbContext context)
    {
        var repository = new CabinConfigurationRepository(context);
        _createUseCase = new CreateCabinConfigurationUseCase(repository);
        _getAllUseCase = new GetAllCabinConfigurationsUseCase(repository);
        _getByIdUseCase = new GetCabinConfigurationByIdUseCase(repository);
        _updateUseCase = new UpdateCabinConfigurationUseCase(repository);
        _deleteUseCase = new DeleteCabinConfigurationUseCase(repository);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Cabin Configuration Management ===");
            Console.WriteLine("1. Create Cabin Configuration");
            Console.WriteLine("2. View All Cabin Configurations");
            Console.WriteLine("3. View Cabin Configuration by ID");
            Console.WriteLine("4. Update Cabin Configuration");
            Console.WriteLine("5. Delete Cabin Configuration");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateCabinConfigurationAsync();
                    break;
                case "2":
                    await ViewAllCabinConfigurationsAsync();
                    break;
                case "3":
                    await ViewCabinConfigurationByIdAsync();
                    break;
                case "4":
                    await UpdateCabinConfigurationAsync();
                    break;
                case "5":
                    await DeleteCabinConfigurationAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task CreateCabinConfigurationAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Cabin Configuration ===");

        try
        {
            Console.Write("Aircraft ID: ");
            var aircraftId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Cabin Type ID: ");
            var cabinTypeId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Start Row: ");
            var startRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("End Row: ");
            var endRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Seats Per Row: ");
            var seatsPerRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Seat Letters: ");
            var seatLetters = Console.ReadLine() ?? string.Empty;

            var cabinConfiguration = await _createUseCase.ExecuteAsync(
                aircraftId,
                cabinTypeId,
                startRow,
                endRow,
                seatsPerRow,
                seatLetters);

            Console.WriteLine($"Cabin configuration created with ID: {cabinConfiguration.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewAllCabinConfigurationsAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Cabin Configurations ===");

        try
        {
            var cabinConfigurations = await _getAllUseCase.ExecuteAsync();
            foreach (var configuration in cabinConfigurations)
            {
                Console.WriteLine($"ID: {configuration.Id.Value}, Aircraft ID: {configuration.AircraftId.Value}, Cabin Type ID: {configuration.CabinTypeId.Value}, Rows: {configuration.StartRow.Value}-{configuration.EndRow.Value}, Seats/Row: {configuration.SeatsPerRow.Value}, Letters: {configuration.SeatLetters.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewCabinConfigurationByIdAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Cabin Configuration by ID ===");

        try
        {
            Console.Write("Cabin Configuration ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var configuration = await _getByIdUseCase.ExecuteAsync(CabinConfigurationId.Create(id));
            if (configuration != null)
            {
                Console.WriteLine($"ID: {configuration.Id.Value}");
                Console.WriteLine($"Aircraft ID: {configuration.AircraftId.Value}");
                Console.WriteLine($"Cabin Type ID: {configuration.CabinTypeId.Value}");
                Console.WriteLine($"Start Row: {configuration.StartRow.Value}");
                Console.WriteLine($"End Row: {configuration.EndRow.Value}");
                Console.WriteLine($"Seats Per Row: {configuration.SeatsPerRow.Value}");
                Console.WriteLine($"Seat Letters: {configuration.SeatLetters.Value}");
            }
            else
            {
                Console.WriteLine("Cabin configuration not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateCabinConfigurationAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Update Cabin Configuration ===");

        try
        {
            Console.Write("Cabin Configuration ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Aircraft ID: ");
            var aircraftId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Cabin Type ID: ");
            var cabinTypeId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Start Row: ");
            var startRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("End Row: ");
            var endRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Seats Per Row: ");
            var seatsPerRow = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Seat Letters: ");
            var seatLetters = Console.ReadLine() ?? string.Empty;

            await _updateUseCase.ExecuteAsync(
                CabinConfigurationId.Create(id),
                aircraftId,
                cabinTypeId,
                startRow,
                endRow,
                seatsPerRow,
                seatLetters);

            Console.WriteLine("Cabin configuration updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteCabinConfigurationAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Cabin Configuration ===");

        try
        {
            Console.Write("Cabin Configuration ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(CabinConfigurationId.Create(id));
            Console.WriteLine("Cabin configuration deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
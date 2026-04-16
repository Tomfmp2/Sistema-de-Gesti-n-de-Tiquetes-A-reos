using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.UI;

public class AircraftUI : IModuleUI
{
    private readonly CreateAircraftUseCase _createUseCase;
    private readonly GetAllAircraftUseCase _getAllUseCase;
    private readonly GetAircraftByIdUseCase _getByIdUseCase;
    private readonly UpdateAircraftUseCase _updateUseCase;
    private readonly DeleteAircraftUseCase _deleteUseCase;

    public AircraftUI(AppDbContext context)
    {
        var repository = new AircraftRepository(context);
        _createUseCase = new CreateAircraftUseCase(repository);
        _getAllUseCase = new GetAllAircraftUseCase(repository);
        _getByIdUseCase = new GetAircraftByIdUseCase(repository);
        _updateUseCase = new UpdateAircraftUseCase(repository);
        _deleteUseCase = new DeleteAircraftUseCase(repository);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Aircraft Management ===");
            Console.WriteLine("1. Create Aircraft");
            Console.WriteLine("2. View All Aircraft");
            Console.WriteLine("3. View Aircraft by ID");
            Console.WriteLine("4. Update Aircraft");
            Console.WriteLine("5. Delete Aircraft");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateAircraftAsync();
                    break;
                case "2":
                    await ViewAllAircraftAsync();
                    break;
                case "3":
                    await ViewAircraftByIdAsync();
                    break;
                case "4":
                    await UpdateAircraftAsync();
                    break;
                case "5":
                    await DeleteAircraftAsync();
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

    private async Task CreateAircraftAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Aircraft ===");

        try
        {
            Console.Write("Model ID: ");
            var modelId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Airline ID: ");
            var airlineId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Registration: ");
            var registration = Console.ReadLine() ?? "";

            Console.Write("Manufacturing Date (yyyy-MM-dd, optional): ");
            var dateInput = Console.ReadLine();
            DateOnly? manufacturingDate = null;
            if (!string.IsNullOrWhiteSpace(dateInput))
            {
                manufacturingDate = DateOnly.Parse(dateInput);
            }

            Console.Write("Is Active (true/false): ");
            var isActive = bool.Parse(Console.ReadLine() ?? "true");

            var aircraft = await _createUseCase.ExecuteAsync(
                ModelId.Create(modelId),
                AirlineId.Create(airlineId),
                Registration.Create(registration),
                manufacturingDate.HasValue ? ManufacturingDate.Create(manufacturingDate.Value) : null,
                IsActive.Create(isActive)
            );

            Console.WriteLine($"Aircraft created with ID: {aircraft.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewAllAircraftAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Aircraft ===");

        try
        {
            var aircraft = await _getAllUseCase.ExecuteAsync();
            foreach (var a in aircraft)
            {
                Console.WriteLine($"ID: {a.Id.Value}, Registration: {a.Registration.Value}, Active: {a.IsActive.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewAircraftByIdAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Aircraft by ID ===");

        try
        {
            Console.Write("Aircraft ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var aircraft = await _getByIdUseCase.ExecuteAsync(AircraftId.Create(id));
            if (aircraft != null)
            {
                Console.WriteLine($"ID: {aircraft.Id.Value}");
                Console.WriteLine($"Model ID: {aircraft.ModelId.Value}");
                Console.WriteLine($"Airline ID: {aircraft.AirlineId.Value}");
                Console.WriteLine($"Registration: {aircraft.Registration.Value}");
                Console.WriteLine($"Manufacturing Date: {aircraft.ManufacturingDate?.Value.ToString("yyyy-MM-dd") ?? "N/A"}");
                Console.WriteLine($"Is Active: {aircraft.IsActive.Value}");
            }
            else
            {
                Console.WriteLine("Aircraft not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateAircraftAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Update Aircraft ===");

        try
        {
            Console.Write("Aircraft ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Model ID: ");
            var modelId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Airline ID: ");
            var airlineId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Registration: ");
            var registration = Console.ReadLine() ?? "";

            Console.Write("Manufacturing Date (yyyy-MM-dd, optional): ");
            var dateInput = Console.ReadLine();
            DateOnly? manufacturingDate = null;
            if (!string.IsNullOrWhiteSpace(dateInput))
            {
                manufacturingDate = DateOnly.Parse(dateInput);
            }

            Console.Write("Is Active (true/false): ");
            var isActive = bool.Parse(Console.ReadLine() ?? "true");

            await _updateUseCase.ExecuteAsync(
                AircraftId.Create(id),
                ModelId.Create(modelId),
                AirlineId.Create(airlineId),
                Registration.Create(registration),
                manufacturingDate.HasValue ? ManufacturingDate.Create(manufacturingDate.Value) : null,
                IsActive.Create(isActive)
            );

            Console.WriteLine("Aircraft updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteAircraftAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Aircraft ===");

        try
        {
            Console.Write("Aircraft ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(AircraftId.Create(id));
            Console.WriteLine("Aircraft deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
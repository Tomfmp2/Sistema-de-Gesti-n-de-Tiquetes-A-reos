namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.UI;

using Application.UseCases;
using Shared.Ui;

public class BaggageTypeConsoleUI : IModuleUI
{
    private readonly CreateBaggageTypeUseCase _createUseCase;
    private readonly GetBaggageTypeByIdUseCase _getByIdUseCase;
    private readonly GetAllBaggageTypesUseCase _getAllUseCase;
    private readonly UpdateBaggageTypeUseCase _updateUseCase;
    private readonly DeleteBaggageTypeUseCase _deleteUseCase;

    public BaggageTypeConsoleUI(
        CreateBaggageTypeUseCase createUseCase,
        GetBaggageTypeByIdUseCase getByIdUseCase,
        GetAllBaggageTypesUseCase getAllUseCase,
        UpdateBaggageTypeUseCase updateUseCase,
        DeleteBaggageTypeUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getByIdUseCase = getByIdUseCase;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════╗");
            Console.WriteLine("║    BAGGAGE TYPES MANAGEMENT SYSTEM         ║");
            Console.WriteLine("╠════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Create Baggage Type                     ║");
            Console.WriteLine("║ 2. Get Baggage Type by ID                  ║");
            Console.WriteLine("║ 3. List All Baggage Types                  ║");
            Console.WriteLine("║ 4. Update Baggage Type                     ║");
            Console.WriteLine("║ 5. Delete Baggage Type                     ║");
            Console.WriteLine("║ 0. Exit                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.Write("Select option: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Invalid input. Press any key to continue...");
                Console.ReadKey();
                continue;
            }

            try
            {
                switch (option)
                {
                    case 1:
                        await CreateBaggageTypeAsync();
                        break;
                    case 2:
                        await GetBaggageTypeByIdAsync();
                        break;
                    case 3:
                        await ListAllBaggageTypesAsync();
                        break;
                    case 4:
                        await UpdateBaggageTypeAsync();
                        break;
                    case 5:
                        await DeleteBaggageTypeAsync();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.ReadKey();
            }
        }
    }

    private async Task CreateBaggageTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Baggage Type ===");

        Console.Write("Name (e.g., Checked Baggage): ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Max Weight (kg): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal maxWeight))
        {
            Console.WriteLine("Invalid weight format.");
            Console.ReadKey();
            return;
        }

        Console.Write("Base Price (amount): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal basePrice))
        {
            Console.WriteLine("Invalid price format.");
            Console.ReadKey();
            return;
        }

        var baggageType = await _createUseCase.ExecuteAsync(name, maxWeight, basePrice);
        Console.WriteLine($"✓ Baggage Type created successfully! ID: {baggageType.Id}");
        Console.ReadKey();
    }

    private async Task GetBaggageTypeByIdAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Get Baggage Type by ID ===");

        Console.Write("Enter Baggage Type ID: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            Console.ReadKey();
            return;
        }

        var baggageType = await _getByIdUseCase.ExecuteAsync(id);
        Console.WriteLine($"\nID: {baggageType.Id}");
        Console.WriteLine($"Name: {baggageType.Name}");
        Console.WriteLine($"Max Weight: {baggageType.MaxWeightKg} kg");
        Console.WriteLine($"Base Price: ${baggageType.BasePrice}");
        Console.ReadKey();
    }

    private async Task ListAllBaggageTypesAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Baggage Types ===\n");

        var baggageTypes = await _getAllUseCase.ExecuteAsync();

        if (baggageTypes.Count == 0)
        {
            Console.WriteLine("No baggage types found.");
        }
        else
        {
            Console.WriteLine($"{"ID",-5} {"Name",-25} {"Max Weight (kg)",-15} {"Base Price",-12}");
            Console.WriteLine("────────────────────────────────────────────────────────");

            foreach (var bt in baggageTypes)
            {
                Console.WriteLine($"{bt.Id.Value,-5} {bt.Name.Value,-25} {bt.MaxWeightKg.Value,-15} ${bt.BasePrice.Value,-11}");
            }
        }

        Console.ReadKey();
    }

    private async Task UpdateBaggageTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Update Baggage Type ===");

        Console.Write("Enter Baggage Type ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            Console.ReadKey();
            return;
        }

        Console.Write("New Name (leave empty to skip): ");
        string? newName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newName))
            newName = null;

        Console.Write("New Max Weight (leave empty to skip): ");
        decimal? newMaxWeight = null;
        string? weightInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(weightInput) && decimal.TryParse(weightInput, out decimal weight))
            newMaxWeight = weight;

        Console.Write("New Base Price (leave empty to skip): ");
        decimal? newBasePrice = null;
        string? priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price))
            newBasePrice = price;

        var baggageType = await _updateUseCase.ExecuteAsync(id, newName, newMaxWeight, newBasePrice);
        Console.WriteLine($"✓ Baggage Type updated successfully!");
        Console.ReadKey();
    }

    private async Task DeleteBaggageTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Baggage Type ===");

        Console.Write("Enter Baggage Type ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID format.");
            Console.ReadKey();
            return;
        }

        Console.Write("Are you sure? (y/n): ");
        if (Console.ReadLine()?.ToLower() != "y")
        {
            Console.WriteLine("Delete cancelled.");
            Console.ReadKey();
            return;
        }

        await _deleteUseCase.ExecuteAsync(id);
        Console.WriteLine($"✓ Baggage Type deleted successfully!");
        Console.ReadKey();
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.UI;

public sealed class BaggageConsoleUI : IModuleUI
{
    private readonly CreateBaggageUseCase _createUseCase;
    private readonly GetBaggageByIdUseCase _getByIdUseCase;
    private readonly GetAllBaggagesUseCase _getAllUseCase;
    private readonly UpdateBaggageUseCase _updateUseCase;
    private readonly DeleteBaggageUseCase _deleteUseCase;

    public BaggageConsoleUI(
        CreateBaggageUseCase createUseCase,
        GetBaggageByIdUseCase getByIdUseCase,
        GetAllBaggagesUseCase getAllUseCase,
        UpdateBaggageUseCase updateUseCase,
        DeleteBaggageUseCase deleteUseCase)
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
            Console.WriteLine("\n╔══════════════════════════════════════╗");
            Console.WriteLine("║      BAGGAGE MANAGEMENT MODULE       ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("1. Create Baggage");
            Console.WriteLine("2. Get Baggage by ID");
            Console.WriteLine("3. List All Baggages");
            Console.WriteLine("4. Update Baggage");
            Console.WriteLine("5. Delete Baggage");
            Console.WriteLine("0. Exit");
            Console.Write("\nSelect option: ");

            string? option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateAsync();
                    break;
                case "2":
                    await GetByIdAsync();
                    break;
                case "3":
                    await ListAllAsync();
                    break;
                case "4":
                    await UpdateAsync();
                    break;
                case "5":
                    await DeleteAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateAsync()
    {
        try
        {
            Console.WriteLine("\n--- Create Baggage ---");
            Console.Write("Checkin ID: ");
            int checkinId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Baggage Type ID: ");
            int baggageTypeId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Weight (kg): ");
            decimal weight = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Charged Price: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

            await _createUseCase.ExecuteAsync(checkinId, baggageTypeId, weight, price);
            Console.WriteLine("✓ Baggage created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    private async Task GetByIdAsync()
    {
        try
        {
            Console.WriteLine("\n--- Get Baggage by ID ---");
            Console.Write("Baggage ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var baggage = await _getByIdUseCase.ExecuteAsync(id);
            Console.WriteLine($"\n┌─────────────────────────────────┐");
            Console.WriteLine($"│ ID: {baggage.Id.Value}");
            Console.WriteLine($"│ Checkin ID: {baggage.CheckinId}");
            Console.WriteLine($"│ Baggage Type ID: {baggage.BaggageTypeId}");
            Console.WriteLine($"│ Weight: {baggage.WeightKg.Value} kg");
            Console.WriteLine($"│ Charged Price: ${baggage.ChargedPrice.Value}");
            Console.WriteLine($"│ Created: {baggage.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"│ Updated: {baggage.UpdatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"└─────────────────────────────────┘");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    private async Task ListAllAsync()
    {
        try
        {
            Console.WriteLine("\n--- All Baggages ---");
            var baggages = await _getAllUseCase.ExecuteAsync();

            if (baggages.Count == 0)
            {
                Console.WriteLine("No baggages found.");
                return;
            }

            foreach (var baggage in baggages)
            {
                Console.WriteLine($"\n┌─────────────────────────────────┐");
                Console.WriteLine($"│ ID: {baggage.Id.Value}");
                Console.WriteLine($"│ Checkin ID: {baggage.CheckinId}");
                Console.WriteLine($"│ Baggage Type ID: {baggage.BaggageTypeId}");
                Console.WriteLine($"│ Weight: {baggage.WeightKg.Value} kg");
                Console.WriteLine($"│ Charged Price: ${baggage.ChargedPrice.Value}");
                Console.WriteLine($"│ Created: {baggage.CreatedAt:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"└─────────────────────────────────┘");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    private async Task UpdateAsync()
    {
        try
        {
            Console.WriteLine("\n--- Update Baggage ---");
            Console.Write("Baggage ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("New Baggage Type ID (or press Enter to skip): ");
            string? baggageTypeIdStr = Console.ReadLine();
            int? baggageTypeId = string.IsNullOrWhiteSpace(baggageTypeIdStr) ? null : int.Parse(baggageTypeIdStr);

            Console.Write("New Weight in kg (or press Enter to skip): ");
            string? weightStr = Console.ReadLine();
            decimal? weight = string.IsNullOrWhiteSpace(weightStr) ? null : decimal.Parse(weightStr);

            Console.Write("New Charged Price (or press Enter to skip): ");
            string? priceStr = Console.ReadLine();
            decimal? price = string.IsNullOrWhiteSpace(priceStr) ? null : decimal.Parse(priceStr);

            await _updateUseCase.ExecuteAsync(id, baggageTypeId, weight, price);
            Console.WriteLine("✓ Baggage updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }

    private async Task DeleteAsync()
    {
        try
        {
            Console.WriteLine("\n--- Delete Baggage ---");
            Console.Write("Baggage ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(id);
            Console.WriteLine("✓ Baggage deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error: {ex.Message}");
        }
    }
}

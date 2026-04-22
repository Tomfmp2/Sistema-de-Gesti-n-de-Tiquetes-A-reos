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
            Console.WriteLine("║      Módulo de equipaje              ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.WriteLine("1. Registrar equipaje");
            Console.WriteLine("2. Consultar equipaje por ID");
            Console.WriteLine("3. Listar todo el equipaje");
            Console.WriteLine("4. Actualizar equipaje");
            Console.WriteLine("5. Eliminar equipaje");
            Console.WriteLine("0. Volver");
            Console.Write("\nElija una opción: ");

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
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private async Task CreateAsync()
    {
        try
        {
            Console.WriteLine("\n--- Registrar equipaje ---");
            Console.Write("ID check-in: ");
            int checkinId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("ID tipo de equipaje: ");
            int baggageTypeId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Peso (kg): ");
            decimal weight = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Precio cobrado: ");
            decimal price = decimal.Parse(Console.ReadLine() ?? "0");

            await _createUseCase.ExecuteAsync(checkinId, baggageTypeId, weight, price);
            Console.WriteLine("Equipaje registrado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetByIdAsync()
    {
        try
        {
            Console.WriteLine("\n--- Consultar equipaje por ID ---");
            Console.Write("ID equipaje: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var baggage = await _getByIdUseCase.ExecuteAsync(id);
            Console.WriteLine($"\n┌─────────────────────────────────┐");
            Console.WriteLine($"│ ID: {baggage.Id.Value}");
            Console.WriteLine($"│ ID check-in: {baggage.CheckinId}");
            Console.WriteLine($"│ ID tipo equipaje: {baggage.BaggageTypeId}");
            Console.WriteLine($"│ Peso: {baggage.WeightKg.Value} kg");
            Console.WriteLine($"│ Precio cobrado: ${baggage.ChargedPrice.Value}");
            Console.WriteLine($"│ Creado: {baggage.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"│ Actualizado: {baggage.UpdatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"└─────────────────────────────────┘");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task ListAllAsync()
    {
        try
        {
            Console.WriteLine("\n--- Todo el equipaje ---");
            var baggages = await _getAllUseCase.ExecuteAsync();

            if (baggages.Count == 0)
            {
                Console.WriteLine("No hay equipajes registrados.");
                return;
            }

            foreach (var baggage in baggages)
            {
                Console.WriteLine($"\n┌─────────────────────────────────┐");
                Console.WriteLine($"│ ID: {baggage.Id.Value}");
                Console.WriteLine($"│ ID check-in: {baggage.CheckinId}");
                Console.WriteLine($"│ ID tipo equipaje: {baggage.BaggageTypeId}");
                Console.WriteLine($"│ Peso: {baggage.WeightKg.Value} kg");
                Console.WriteLine($"│ Precio cobrado: ${baggage.ChargedPrice.Value}");
                Console.WriteLine($"│ Creado: {baggage.CreatedAt:yyyy-MM-dd HH:mm:ss}");
                Console.WriteLine($"└─────────────────────────────────┘");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task UpdateAsync()
    {
        try
        {
            Console.WriteLine("\n--- Actualizar equipaje ---");
            Console.Write("ID equipaje: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Nuevo ID tipo de equipaje (Enter para omitir): ");
            string? baggageTypeIdStr = Console.ReadLine();
            int? baggageTypeId = string.IsNullOrWhiteSpace(baggageTypeIdStr) ? null : int.Parse(baggageTypeIdStr);

            Console.Write("Nuevo peso en kg (Enter para omitir): ");
            string? weightStr = Console.ReadLine();
            decimal? weight = string.IsNullOrWhiteSpace(weightStr) ? null : decimal.Parse(weightStr);

            Console.Write("Nuevo precio cobrado (Enter para omitir): ");
            string? priceStr = Console.ReadLine();
            decimal? price = string.IsNullOrWhiteSpace(priceStr) ? null : decimal.Parse(priceStr);

            await _updateUseCase.ExecuteAsync(id, baggageTypeId, weight, price);
            Console.WriteLine("Equipaje actualizado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteAsync()
    {
        try
        {
            Console.WriteLine("\n--- Eliminar equipaje ---");
            Console.Write("ID equipaje: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(id);
            Console.WriteLine("Equipaje eliminado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

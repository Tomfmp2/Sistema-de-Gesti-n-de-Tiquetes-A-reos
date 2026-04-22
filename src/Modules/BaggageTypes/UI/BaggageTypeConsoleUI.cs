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
            Console.WriteLine("║    Tipos de equipaje                      ║");
            Console.WriteLine("╠════════════════════════════════════════════╣");
            Console.WriteLine("║ 1. Crear tipo de equipaje                  ║");
            Console.WriteLine("║ 2. Consultar tipo por ID                   ║");
            Console.WriteLine("║ 3. Listar todos los tipos                  ║");
            Console.WriteLine("║ 4. Actualizar tipo                         ║");
            Console.WriteLine("║ 5. Eliminar tipo                           ║");
            Console.WriteLine("║ 0. Volver                                  ║");
            Console.WriteLine("╚════════════════════════════════════════════╝");
            Console.Write("Elija una opción: ");

            if (!int.TryParse(Console.ReadLine(), out int option))
            {
                Console.WriteLine("Entrada no válida. Pulse una tecla para continuar…");
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
                        Console.WriteLine("Opción no válida. Pulse una tecla para continuar…");
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
        Console.WriteLine("=== Crear tipo de equipaje ===");

        Console.Write("Nombre (p. ej. equipaje de bodega): ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Peso máximo (kg): ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal maxWeight))
        {
            Console.WriteLine("Formato de peso no válido.");
            Console.ReadKey();
            return;
        }

        Console.Write("Precio base: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal basePrice))
        {
            Console.WriteLine("Formato de precio no válido.");
            Console.ReadKey();
            return;
        }

        var baggageType = await _createUseCase.ExecuteAsync(name, maxWeight, basePrice);
        Console.WriteLine($"Tipo de equipaje creado. ID: {baggageType.Id}");
        Console.ReadKey();
    }

    private async Task GetBaggageTypeByIdAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Consultar tipo por ID ===");

        Console.Write("ID tipo de equipaje: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Formato de ID no válido.");
            Console.ReadKey();
            return;
        }

        var baggageType = await _getByIdUseCase.ExecuteAsync(id);
        Console.WriteLine($"\nID: {baggageType.Id.Value}");
        Console.WriteLine($"Nombre: {baggageType.Name.Value}");
        Console.WriteLine($"Peso máx.: {baggageType.MaxWeightKg.Value} kg");
        Console.WriteLine($"Precio base: ${baggageType.BasePrice.Value}");
        Console.ReadKey();
    }

    private async Task ListAllBaggageTypesAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Tipos de equipaje ===\n");

        var baggageTypes = await _getAllUseCase.ExecuteAsync();

        if (baggageTypes.Count == 0)
        {
            Console.WriteLine("No hay tipos de equipaje registrados.");
        }
        else
        {
            Console.WriteLine($"{"ID",-5} {"Nombre",-25} {"Peso máx. (kg)",-15} {"Precio base",-12}");
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
        Console.WriteLine("=== Actualizar tipo de equipaje ===");

        Console.Write("ID del tipo a actualizar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Formato de ID no válido.");
            Console.ReadKey();
            return;
        }

        Console.Write("Nuevo nombre (vacío para omitir): ");
        string? newName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newName))
            newName = null;

        Console.Write("Nuevo peso máximo (vacío para omitir): ");
        decimal? newMaxWeight = null;
        string? weightInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(weightInput) && decimal.TryParse(weightInput, out decimal weight))
            newMaxWeight = weight;

        Console.Write("Nuevo precio base (vacío para omitir): ");
        decimal? newBasePrice = null;
        string? priceInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(priceInput) && decimal.TryParse(priceInput, out decimal price))
            newBasePrice = price;

        var baggageType = await _updateUseCase.ExecuteAsync(id, newName, newMaxWeight, newBasePrice);
        Console.WriteLine("Tipo de equipaje actualizado correctamente.");
        Console.ReadKey();
    }

    private async Task DeleteBaggageTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Eliminar tipo de equipaje ===");

        Console.Write("ID del tipo a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Formato de ID no válido.");
            Console.ReadKey();
            return;
        }

        Console.Write("¿Confirma la eliminación? (s/n): ");
        var confirm = Console.ReadLine()?.Trim().ToLowerInvariant();
        if (confirm is not "y" and not "s")
        {
            Console.WriteLine("Eliminación cancelada.");
            Console.ReadKey();
            return;
        }

        await _deleteUseCase.ExecuteAsync(id);
        Console.WriteLine("Tipo de equipaje eliminado correctamente.");
        Console.ReadKey();
    }
}

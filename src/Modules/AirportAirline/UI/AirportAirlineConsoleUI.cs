using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.UI;

public class AirportAirlineConsoleUI : IModuleUI
{
    private readonly CreateAirportAirlineUseCase _createUseCase;
    private readonly GetAirportAirlineByIdUseCase _getByIdUseCase;
    private readonly GetAllAirportAirlinesUseCase _getAllUseCase;
    private readonly UpdateAirportAirlineUseCase _updateUseCase;
    private readonly DeleteAirportAirlineUseCase _deleteUseCase;

    public AirportAirlineConsoleUI(CreateAirportAirlineUseCase create, GetAirportAirlineByIdUseCase getById, GetAllAirportAirlinesUseCase getAll, UpdateAirportAirlineUseCase update, DeleteAirportAirlineUseCase delete)
    {
        _createUseCase = create;
        _getByIdUseCase = getById;
        _getAllUseCase = getAll;
        _updateUseCase = update;
        _deleteUseCase = delete;
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Aeropuerto ↔ Aerolínea", "Operación por terminal y fechas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear relación", CreateAirportAirline),
                ("Consultar por ID", GetAirportAirlineById),
                ("Listar todas", GetAllAirportAirlines),
                ("Actualizar", UpdateAirportAirline),
                ("Eliminar", DeleteAirportAirline),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private void CreateAirportAirline()
    {
        Console.Write("ID aeropuerto: ");
        var airportId = int.Parse(Console.ReadLine()!);
        Console.Write("ID aerolínea: ");
        var airlineId = int.Parse(Console.ReadLine()!);
        Console.Write("Terminal (opcional): ");
        var terminal = Console.ReadLine();
        Console.Write("Fecha inicio (yyyy-MM-dd): ");
        var startDate = DateOnly.Parse(Console.ReadLine()!);
        Console.Write("Fecha fin (yyyy-MM-dd o vacío): ");
        var endDateStr = Console.ReadLine();
        DateOnly? endDate = string.IsNullOrWhiteSpace(endDateStr) ? null : DateOnly.Parse(endDateStr);
        try
        {
            var airportAirline = _createUseCase.ExecuteAsync(airportId, airlineId, string.IsNullOrWhiteSpace(terminal) ? null : terminal, startDate, endDate).Result;
            Console.WriteLine($"Relación aeropuerto-aerolínea creada (ID: {airportAirline.Id.Value})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void GetAirportAirlineById()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            var airportAirline = _getByIdUseCase.ExecuteAsync(id).Result;
            if (airportAirline != null)
            {
                Console.WriteLine($"ID: {airportAirline.Id.Value}, Aeropuerto: {airportAirline.AirportId}, Aerolínea: {airportAirline.AirlineId}, Terminal: {airportAirline.Terminal.Value}");
            }
            else
            {
                Console.WriteLine("No encontrado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void GetAllAirportAirlines()
    {
        try
        {
            var airportAirlines = _getAllUseCase.ExecuteAsync().Result.ToList();
            if (airportAirlines.Count == 0)
            {
                Console.WriteLine("No hay relaciones para mostrar.");
                SpectreUi.Pause();
                return;
            }

            foreach (var aa in airportAirlines)
            {
                Console.WriteLine($"ID: {aa.Id.Value}, Aeropuerto: {aa.AirportId}, Aerolínea: {aa.AirlineId}, Terminal: {aa.Terminal.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void UpdateAirportAirline()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("ID aeropuerto: ");
        var airportId = int.Parse(Console.ReadLine()!);
        Console.Write("ID aerolínea: ");
        var airlineId = int.Parse(Console.ReadLine()!);
        Console.Write("Terminal (opcional): ");
        var terminal = Console.ReadLine();
        Console.Write("Fecha inicio (yyyy-MM-dd): ");
        var startDate = DateOnly.Parse(Console.ReadLine()!);
        Console.Write("Fecha fin (yyyy-MM-dd o vacío): ");
        var endDateStr = Console.ReadLine();
        DateOnly? endDate = string.IsNullOrWhiteSpace(endDateStr) ? null : DateOnly.Parse(endDateStr);
        Console.Write("¿Activa? (true/false): ");
        var isActive = bool.Parse(Console.ReadLine()!);
        try
        {
            _updateUseCase.ExecuteAsync(id, airportId, airlineId, string.IsNullOrWhiteSpace(terminal) ? null : terminal, startDate, endDate, isActive).Wait();
            Console.WriteLine("Actualizado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void DeleteAirportAirline()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            _deleteUseCase.ExecuteAsync(id).Wait();
            Console.WriteLine("Eliminado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }
}
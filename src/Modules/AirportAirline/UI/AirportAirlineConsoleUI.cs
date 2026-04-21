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
        Console.Write("Airport ID: ");
        var airportId = int.Parse(Console.ReadLine()!);
        Console.Write("Airline ID: ");
        var airlineId = int.Parse(Console.ReadLine()!);
        Console.Write("Terminal (optional): ");
        var terminal = Console.ReadLine();
        Console.Write("Start Date (yyyy-MM-dd): ");
        var startDate = DateOnly.Parse(Console.ReadLine()!);
        Console.Write("End Date (yyyy-MM-dd or empty): ");
        var endDateStr = Console.ReadLine();
        DateOnly? endDate = string.IsNullOrWhiteSpace(endDateStr) ? null : DateOnly.Parse(endDateStr);
        try
        {
            var airportAirline = _createUseCase.ExecuteAsync(airportId, airlineId, string.IsNullOrWhiteSpace(terminal) ? null : terminal, startDate, endDate).Result;
            Console.WriteLine($"Created Airport-Airline: {airportAirline.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void GetAirportAirlineById()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        var airportAirline = _getByIdUseCase.ExecuteAsync(id).Result;
        if (airportAirline != null)
        {
            Console.WriteLine($"ID: {airportAirline.Id.Value}, Airport: {airportAirline.AirportId}, Airline: {airportAirline.AirlineId}, Terminal: {airportAirline.Terminal.Value}");
        }
        else
        {
            Console.WriteLine("Not found");
        }
    }

    private void GetAllAirportAirlines()
    {
        var airportAirlines = _getAllUseCase.ExecuteAsync().Result;
        foreach (var aa in airportAirlines)
        {
            Console.WriteLine($"ID: {aa.Id.Value}, Airport: {aa.AirportId}, Airline: {aa.AirlineId}, Terminal: {aa.Terminal.Value}");
        }
    }

    private void UpdateAirportAirline()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Airport ID: ");
        var airportId = int.Parse(Console.ReadLine()!);
        Console.Write("Airline ID: ");
        var airlineId = int.Parse(Console.ReadLine()!);
        Console.Write("Terminal (optional): ");
        var terminal = Console.ReadLine();
        Console.Write("Start Date (yyyy-MM-dd): ");
        var startDate = DateOnly.Parse(Console.ReadLine()!);
        Console.Write("End Date (yyyy-MM-dd or empty): ");
        var endDateStr = Console.ReadLine();
        DateOnly? endDate = string.IsNullOrWhiteSpace(endDateStr) ? null : DateOnly.Parse(endDateStr);
        Console.Write("Is Active (true/false): ");
        var isActive = bool.Parse(Console.ReadLine()!);
        try
        {
            _updateUseCase.ExecuteAsync(id, airportId, airlineId, string.IsNullOrWhiteSpace(terminal) ? null : terminal, startDate, endDate, isActive).Wait();
            Console.WriteLine("Updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void DeleteAirportAirline()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        _deleteUseCase.ExecuteAsync(id).Wait();
        Console.WriteLine("Deleted");
    }
}
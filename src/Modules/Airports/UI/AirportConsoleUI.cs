using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.UI;

public class AirportConsoleUI : IModuleUI
{
    private readonly CreateAirportUseCase _createUseCase;
    private readonly GetAirportByIdUseCase _getByIdUseCase;
    private readonly GetAllAirportsUseCase _getAllUseCase;
    private readonly UpdateAirportUseCase _updateUseCase;
    private readonly DeleteAirportUseCase _deleteUseCase;

    public AirportConsoleUI(CreateAirportUseCase create, GetAirportByIdUseCase getById, GetAllAirportsUseCase getAll, UpdateAirportUseCase update, DeleteAirportUseCase delete)
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
            SpectreUi.ModuleHeader("Aeropuertos", "IATA / ICAO y ciudad");

            var items = new (string Label, Action Action)[]
            {
                ("Crear aeropuerto", CreateAirport),
                ("Consultar por ID", GetAirportById),
                ("Listar todos", GetAllAirports),
                ("Actualizar", UpdateAirport),
                ("Eliminar", DeleteAirport),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private void CreateAirport()
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("IATA Code: ");
        var iata = Console.ReadLine();
        Console.Write("ICAO Code (optional): ");
        var icao = Console.ReadLine();
        Console.Write("City ID: ");
        var cityId = int.Parse(Console.ReadLine()!);
        try
        {
            var airport = _createUseCase.ExecuteAsync(name!, iata!, string.IsNullOrWhiteSpace(icao) ? null : icao, cityId).Result;
            Console.WriteLine($"Created Airport: {airport.Name.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void GetAirportById()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            var airport = _getByIdUseCase.ExecuteAsync(id).Result;
            if (airport != null)
            {
                Console.WriteLine($"ID: {airport.Id.Value}, Name: {airport.Name.Value}, IATA: {airport.IataCode.Value}, ICAO: {airport.IcaoCode.Value}");
            }
            else
            {
                Console.WriteLine("Not found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void GetAllAirports()
    {
        try
        {
            var airports = _getAllUseCase.ExecuteAsync().Result.ToList();
            if (airports.Count == 0)
            {
                Console.WriteLine("No hay aeropuertos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            foreach (var a in airports)
            {
                Console.WriteLine($"ID: {a.Id.Value}, Name: {a.Name.Value}, IATA: {a.IataCode.Value}, ICAO: {a.IcaoCode.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void UpdateAirport()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("IATA Code: ");
        var iata = Console.ReadLine();
        Console.Write("ICAO Code (optional): ");
        var icao = Console.ReadLine();
        Console.Write("City ID: ");
        var cityId = int.Parse(Console.ReadLine()!);
        try
        {
            _updateUseCase.ExecuteAsync(id, name!, iata!, string.IsNullOrWhiteSpace(icao) ? null : icao, cityId).Wait();
            Console.WriteLine("Updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void DeleteAirport()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            _deleteUseCase.ExecuteAsync(id).Wait();
            Console.WriteLine("Deleted");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }
}
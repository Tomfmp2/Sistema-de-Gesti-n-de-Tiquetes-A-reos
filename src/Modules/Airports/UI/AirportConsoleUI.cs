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
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Código IATA: ");
        var iata = Console.ReadLine();
        Console.Write("Código ICAO (opcional): ");
        var icao = Console.ReadLine();
        Console.Write("ID ciudad: ");
        var cityId = int.Parse(Console.ReadLine()!);
        try
        {
            var airport = _createUseCase.ExecuteAsync(name!, iata!, string.IsNullOrWhiteSpace(icao) ? null : icao, cityId).Result;
            Console.WriteLine($"Aeropuerto creado: {airport.Name.Value}");
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
                Console.WriteLine($"ID: {airport.Id.Value}, Nombre: {airport.Name.Value}, IATA: {airport.IataCode.Value}, ICAO: {airport.IcaoCode.Value}");
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
                Console.WriteLine($"ID: {a.Id.Value}, Nombre: {a.Name.Value}, IATA: {a.IataCode.Value}, ICAO: {a.IcaoCode.Value}");
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
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Código IATA: ");
        var iata = Console.ReadLine();
        Console.Write("Código ICAO (opcional): ");
        var icao = Console.ReadLine();
        Console.Write("ID ciudad: ");
        var cityId = int.Parse(Console.ReadLine()!);
        try
        {
            _updateUseCase.ExecuteAsync(id, name!, iata!, string.IsNullOrWhiteSpace(icao) ? null : icao, cityId).Wait();
            Console.WriteLine("Actualizado");
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
            Console.WriteLine("Eliminado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }
}
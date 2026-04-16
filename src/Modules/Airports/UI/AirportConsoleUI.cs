using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;
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
        while (true)
        {
            Console.WriteLine("Airport Management");
            Console.WriteLine("1. Create Airport");
            Console.WriteLine("2. Get Airport by ID");
            Console.WriteLine("3. Get All Airports");
            Console.WriteLine("4. Update Airport");
            Console.WriteLine("5. Delete Airport");
            Console.WriteLine("0. Back");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateAirport();
                    break;
                case "2":
                    GetAirportById();
                    break;
                case "3":
                    GetAllAirports();
                    break;
                case "4":
                    UpdateAirport();
                    break;
                case "5":
                    DeleteAirport();
                    break;
                case "0":
                    return;
            }
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
    }

    private void GetAirportById()
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

    private void GetAllAirports()
    {
        var airports = _getAllUseCase.ExecuteAsync().Result;
        foreach (var a in airports)
        {
            Console.WriteLine($"ID: {a.Id.Value}, Name: {a.Name.Value}, IATA: {a.IataCode.Value}, ICAO: {a.IcaoCode.Value}");
        }
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
    }

    private void DeleteAirport()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        _deleteUseCase.ExecuteAsync(id).Wait();
        Console.WriteLine("Deleted");
    }
}
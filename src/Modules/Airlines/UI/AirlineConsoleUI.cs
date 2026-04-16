using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.UI;

public class AirlineConsoleUI : IModuleUI
{
    private readonly CreateAirlineUseCase _createUseCase;
    private readonly GetAirlineByIdUseCase _getByIdUseCase;
    private readonly GetAllAirlinesUseCase _getAllUseCase;
    private readonly UpdateAirlineUseCase _updateUseCase;
    private readonly DeleteAirlineUseCase _deleteUseCase;

    public AirlineConsoleUI(CreateAirlineUseCase create, GetAirlineByIdUseCase getById, GetAllAirlinesUseCase getAll, UpdateAirlineUseCase update, DeleteAirlineUseCase delete)
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
            Console.WriteLine("Airline Management");
            Console.WriteLine("1. Create Airline");
            Console.WriteLine("2. Get Airline by ID");
            Console.WriteLine("3. Get All Airlines");
            Console.WriteLine("4. Update Airline");
            Console.WriteLine("5. Delete Airline");
            Console.WriteLine("0. Back");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreateAirline();
                    break;
                case "2":
                    GetAirlineById();
                    break;
                case "3":
                    GetAllAirlines();
                    break;
                case "4":
                    UpdateAirline();
                    break;
                case "5":
                    DeleteAirline();
                    break;
                case "0":
                    return;
            }
        }
    }

    private void CreateAirline()
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("IATA Code: ");
        var iata = Console.ReadLine();
        Console.Write("Origin Country ID: ");
        var countryId = int.Parse(Console.ReadLine()!);
        try
        {
            var airline = _createUseCase.ExecuteAsync(name!, iata!, countryId).Result;
            Console.WriteLine($"Created Airline: {airline.Name.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void GetAirlineById()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        var airline = _getByIdUseCase.ExecuteAsync(id).Result;
        if (airline != null)
        {
            Console.WriteLine($"ID: {airline.Id.Value}, Name: {airline.Name.Value}, IATA: {airline.IataCode.Value}");
        }
        else
        {
            Console.WriteLine("Not found");
        }
    }

    private void GetAllAirlines()
    {
        var airlines = _getAllUseCase.ExecuteAsync().Result;
        foreach (var a in airlines)
        {
            Console.WriteLine($"ID: {a.Id.Value}, Name: {a.Name.Value}, IATA: {a.IataCode.Value}");
        }
    }

    private void UpdateAirline()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Name: ");
        var name = Console.ReadLine();
        Console.Write("IATA Code: ");
        var iata = Console.ReadLine();
        Console.Write("Origin Country ID: ");
        var countryId = int.Parse(Console.ReadLine()!);
        Console.Write("Is Active (true/false): ");
        var isActive = bool.Parse(Console.ReadLine()!);
        try
        {
            _updateUseCase.ExecuteAsync(id, name!, iata!, countryId, isActive).Wait();
            Console.WriteLine("Updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void DeleteAirline()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        _deleteUseCase.ExecuteAsync(id).Wait();
        Console.WriteLine("Deleted");
    }
}
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
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
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Aerolíneas", "Gestión de aerolíneas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear aerolínea", CreateAirline),
                ("Consultar por ID", GetAirlineById),
                ("Listar todas", GetAllAirlines),
                ("Actualizar", UpdateAirline),
                ("Eliminar", DeleteAirline),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private void CreateAirline()
    {
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Código IATA: ");
        var iata = Console.ReadLine();
        Console.Write("ID país de origen: ");
        var countryId = int.Parse(Console.ReadLine()!);
        try
        {
            var airline = _createUseCase.ExecuteAsync(name!, iata!, countryId).Result;
            Console.WriteLine($"Aerolínea creada: {airline.Name.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private void GetAirlineById()
    {
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine()!);
            var airline = _getByIdUseCase.ExecuteAsync(id).Result;
            if (airline != null)
            {
                Console.WriteLine($"ID: {airline.Id.Value}, Nombre: {airline.Name.Value}, IATA: {airline.IataCode.Value}");
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

    private void GetAllAirlines()
    {
        try
        {
            var airlines = _getAllUseCase.ExecuteAsync().Result.ToList();

            if (airlines.Count == 0)
            {
                Console.WriteLine("No hay aerolíneas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            foreach (var a in airlines)
            {
                Console.WriteLine($"ID: {a.Id.Value}, Nombre: {a.Name.Value}, IATA: {a.IataCode.Value}");
            }

            SpectreUi.Pause();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            SpectreUi.Pause();
        }
    }

    private void UpdateAirline()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine()!);
        Console.Write("Nombre: ");
        var name = Console.ReadLine();
        Console.Write("Código IATA: ");
        var iata = Console.ReadLine();
        Console.Write("ID país de origen: ");
        var countryId = int.Parse(Console.ReadLine()!);
        Console.Write("¿Activa? (true/false): ");
        var isActive = bool.Parse(Console.ReadLine()!);
        try
        {
            _updateUseCase.ExecuteAsync(id, name!, iata!, countryId, isActive).Wait();
            Console.WriteLine("Actualizado");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private void DeleteAirline()
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
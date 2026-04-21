using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.UI;

public class AircraftManufacturerUI : IModuleUI
{
    private readonly CreateAircraftManufacturerUseCase _createUseCase;
    private readonly GetAllAircraftManufacturersUseCase _getAllUseCase;
    private readonly GetAircraftManufacturerByIdUseCase _getByIdUseCase;
    private readonly UpdateAircraftManufacturerUseCase _updateUseCase;
    private readonly DeleteAircraftManufacturerUseCase _deleteUseCase;

    public AircraftManufacturerUI(
        CreateAircraftManufacturerUseCase createUseCase,
        GetAllAircraftManufacturersUseCase getAllUseCase,
        GetAircraftManufacturerByIdUseCase getByIdUseCase,
        UpdateAircraftManufacturerUseCase updateUseCase,
        DeleteAircraftManufacturerUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getAllUseCase = getAllUseCase;
        _getByIdUseCase = getByIdUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Fabricantes de aeronaves", "Catálogo de fabricantes");

            var items = new (string Label, Action Action)[]
            {
                ("Crear fabricante", () => CreateAircraftManufacturerAsync().GetAwaiter().GetResult()),
                ("Listar todos", () => ViewAllAircraftManufacturersAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewAircraftManufacturerByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateAircraftManufacturerAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteAircraftManufacturerAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAircraftManufacturerAsync()
    {
        SpectreUi.ModuleHeader("Crear fabricante", null);
        try
        {
            Console.Write("ID del fabricante (entero): ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            Console.Write("Nombre: ");
            var name = Console.ReadLine()!;
            var aircraftManufacturerName = AircraftManufacturerName.Create(name);

            Console.Write("País: ");
            var country = Console.ReadLine()!;
            var countryValue = Country.Create(country);

            await _createUseCase.ExecuteAsync(aircraftManufacturerId, aircraftManufacturerName, countryValue);
            Console.WriteLine("Fabricante creado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllAircraftManufacturersAsync()
    {
        SpectreUi.ModuleHeader("Fabricantes registrados", null);
        var aircraftManufacturers = await _getAllUseCase.ExecuteAsync();
        foreach (var am in aircraftManufacturers)
        {
            Console.WriteLine($"ID: {am.Id.Value}, Nombre: {am.Name.Value}, País: {am.Country.Value}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAircraftManufacturerByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar fabricante", null);
        Console.Write("ID del fabricante: ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftManufacturerId = AircraftManufacturerId.Create(id);

        var aircraftManufacturer = await _getByIdUseCase.ExecuteAsync(aircraftManufacturerId);
        if (aircraftManufacturer != null)
        {
            Console.WriteLine($"ID: {aircraftManufacturer.Id.Value}, Nombre: {aircraftManufacturer.Name.Value}, País: {aircraftManufacturer.Country.Value}");
        }
        else
        {
            Console.WriteLine("No se encontró el fabricante.");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateAircraftManufacturerAsync()
    {
        SpectreUi.ModuleHeader("Actualizar fabricante", null);
        try
        {
            Console.Write("ID del fabricante: ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            Console.Write("Nuevo nombre: ");
            var name = Console.ReadLine()!;
            var aircraftManufacturerName = AircraftManufacturerName.Create(name);

            Console.Write("Nuevo país: ");
            var country = Console.ReadLine()!;
            var countryValue = Country.Create(country);

            await _updateUseCase.ExecuteAsync(aircraftManufacturerId, aircraftManufacturerName, countryValue);
            Console.WriteLine("Fabricante actualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteAircraftManufacturerAsync()
    {
        SpectreUi.ModuleHeader("Eliminar fabricante", null);
        Console.Write("ID del fabricante: ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftManufacturerId = AircraftManufacturerId.Create(id);

        await _deleteUseCase.ExecuteAsync(aircraftManufacturerId);
        Console.WriteLine("Fabricante eliminado.");
        SpectreUi.Pause();
    }
}

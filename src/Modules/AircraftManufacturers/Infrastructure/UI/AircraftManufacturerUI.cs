using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftManufacturers.Infrastructure.UI;

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
        while (true)
        {
            Console.WriteLine("\n=== Aircraft Manufacturers Management ===");
            Console.WriteLine("1. Create Aircraft Manufacturer");
            Console.WriteLine("2. View All Aircraft Manufacturers");
            Console.WriteLine("3. View Aircraft Manufacturer by ID");
            Console.WriteLine("4. Update Aircraft Manufacturer");
            Console.WriteLine("5. Delete Aircraft Manufacturer");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateAircraftManufacturerAsync();
                    break;
                case "2":
                    await ViewAllAircraftManufacturersAsync();
                    break;
                case "3":
                    await ViewAircraftManufacturerByIdAsync();
                    break;
                case "4":
                    await UpdateAircraftManufacturerAsync();
                    break;
                case "5":
                    await DeleteAircraftManufacturerAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateAircraftManufacturerAsync()
    {
        try
        {
            Console.Write("Enter Aircraft Manufacturer ID (int): ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            Console.Write("Enter Name: ");
            var name = Console.ReadLine()!;
            var aircraftManufacturerName = AircraftManufacturerName.Create(name);

            Console.Write("Enter Country: ");
            var country = Console.ReadLine()!;
            var countryValue = Country.Create(country);

            await _createUseCase.ExecuteAsync(aircraftManufacturerId, aircraftManufacturerName, countryValue);
            Console.WriteLine("Aircraft manufacturer created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task ViewAllAircraftManufacturersAsync()
    {
        var aircraftManufacturers = await _getAllUseCase.ExecuteAsync();
        foreach (var am in aircraftManufacturers)
        {
            Console.WriteLine($"ID: {am.Id.Value}, Name: {am.Name.Value}, Country: {am.Country.Value}");
        }
    }

    private async Task ViewAircraftManufacturerByIdAsync()
    {
        Console.Write("Enter Aircraft Manufacturer ID (int): ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftManufacturerId = AircraftManufacturerId.Create(id);

        var aircraftManufacturer = await _getByIdUseCase.ExecuteAsync(aircraftManufacturerId);
        if (aircraftManufacturer != null)
        {
            Console.WriteLine($"ID: {aircraftManufacturer.Id.Value}, Name: {aircraftManufacturer.Name.Value}, Country: {aircraftManufacturer.Country.Value}");
        }
        else
        {
            Console.WriteLine("Aircraft manufacturer not found.");
        }
    }

    private async Task UpdateAircraftManufacturerAsync()
    {
        try
        {
            Console.Write("Enter Aircraft Manufacturer ID (int): ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            Console.Write("Enter new Name: ");
            var name = Console.ReadLine()!;
            var aircraftManufacturerName = AircraftManufacturerName.Create(name);

            Console.Write("Enter new Country: ");
            var country = Console.ReadLine()!;
            var countryValue = Country.Create(country);

            await _updateUseCase.ExecuteAsync(aircraftManufacturerId, aircraftManufacturerName, countryValue);
            Console.WriteLine("Aircraft manufacturer updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteAircraftManufacturerAsync()
    {
        Console.Write("Enter Aircraft Manufacturer ID (int): ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftManufacturerId = AircraftManufacturerId.Create(id);

        await _deleteUseCase.ExecuteAsync(aircraftManufacturerId);
        Console.WriteLine("Aircraft manufacturer deleted successfully.");
    }
}
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.UI;

public class AircraftModelUI : IModuleUI
{
    private readonly CreateAircraftModelUseCase _createUseCase;
    private readonly GetAllAircraftModelsUseCase _getAllUseCase;
    private readonly GetAircraftModelByIdUseCase _getByIdUseCase;
    private readonly UpdateAircraftModelUseCase _updateUseCase;
    private readonly DeleteAircraftModelUseCase _deleteUseCase;

    public AircraftModelUI(
        CreateAircraftModelUseCase createUseCase,
        GetAllAircraftModelsUseCase getAllUseCase,
        GetAircraftModelByIdUseCase getByIdUseCase,
        UpdateAircraftModelUseCase updateUseCase,
        DeleteAircraftModelUseCase deleteUseCase)
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
            Console.WriteLine("\n=== Aircraft Models Management ===");
            Console.WriteLine("1. Create Aircraft Model");
            Console.WriteLine("2. View All Aircraft Models");
            Console.WriteLine("3. View Aircraft Model by ID");
            Console.WriteLine("4. Update Aircraft Model");
            Console.WriteLine("5. Delete Aircraft Model");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Choose an option: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateAircraftModelAsync();
                    break;
                case "2":
                    await ViewAllAircraftModelsAsync();
                    break;
                case "3":
                    await ViewAircraftModelByIdAsync();
                    break;
                case "4":
                    await UpdateAircraftModelAsync();
                    break;
                case "5":
                    await DeleteAircraftModelAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    private async Task CreateAircraftModelAsync()
    {
        try
        {
            Console.Write("Enter Aircraft Model ID (int): ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftModelId = AircraftModelId.Create(id);

            Console.Write("Enter Manufacturer ID (int): ");
            var manufacturerIdValue = int.Parse(Console.ReadLine()!);
            var manufacturerId = ManufacturerId.Create(manufacturerIdValue);

            Console.Write("Enter Model Name: ");
            var modelName = Console.ReadLine()!;
            var modelNameValue = ModelName.Create(modelName);

            Console.Write("Enter Max Capacity (int): ");
            var maxCapacityValue = int.Parse(Console.ReadLine()!);
            var maxCapacity = MaxCapacity.Create(maxCapacityValue);

            Console.Write("Enter Max Takeoff Weight Kg (decimal, optional): ");
            var maxTakeoffWeightInput = Console.ReadLine();
            MaxTakeoffWeightKg? maxTakeoffWeightKg = null;
            if (!string.IsNullOrWhiteSpace(maxTakeoffWeightInput))
            {
                var maxTakeoffWeightValue = decimal.Parse(maxTakeoffWeightInput);
                maxTakeoffWeightKg = MaxTakeoffWeightKg.Create(maxTakeoffWeightValue);
            }

            Console.Write("Enter Fuel Consumption Kg/H (decimal, optional): ");
            var fuelConsumptionInput = Console.ReadLine();
            FuelConsumptionKgH? fuelConsumptionKgH = null;
            if (!string.IsNullOrWhiteSpace(fuelConsumptionInput))
            {
                var fuelConsumptionValue = decimal.Parse(fuelConsumptionInput);
                fuelConsumptionKgH = FuelConsumptionKgH.Create(fuelConsumptionValue);
            }

            Console.Write("Enter Cruising Speed Kmh (int, optional): ");
            var cruisingSpeedInput = Console.ReadLine();
            CruisingSpeedKmh? cruisingSpeedKmh = null;
            if (!string.IsNullOrWhiteSpace(cruisingSpeedInput))
            {
                var cruisingSpeedValue = int.Parse(cruisingSpeedInput);
                cruisingSpeedKmh = CruisingSpeedKmh.Create(cruisingSpeedValue);
            }

            Console.Write("Enter Cruising Altitude Ft (int, optional): ");
            var cruisingAltitudeInput = Console.ReadLine();
            CruisingAltitudeFt? cruisingAltitudeFt = null;
            if (!string.IsNullOrWhiteSpace(cruisingAltitudeInput))
            {
                var cruisingAltitudeValue = int.Parse(cruisingAltitudeInput);
                cruisingAltitudeFt = CruisingAltitudeFt.Create(cruisingAltitudeValue);
            }

            await _createUseCase.ExecuteAsync(aircraftModelId, manufacturerId, modelNameValue, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
            Console.WriteLine("Aircraft model created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task ViewAllAircraftModelsAsync()
    {
        var aircraftModels = await _getAllUseCase.ExecuteAsync();
        foreach (var am in aircraftModels)
        {
            Console.WriteLine($"ID: {am.Id.Value}, Manufacturer ID: {am.ManufacturerId.Value}, Model: {am.ModelName.Value}, Capacity: {am.MaxCapacity.Value}, MTOW: {am.MaxTakeoffWeightKg?.Value ?? 0}, Fuel: {am.FuelConsumptionKgH?.Value ?? 0}, Speed: {am.CruisingSpeedKmh?.Value ?? 0}, Altitude: {am.CruisingAltitudeFt?.Value ?? 0}");
        }
    }

    private async Task ViewAircraftModelByIdAsync()
    {
        Console.Write("Enter Aircraft Model ID (int): ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftModelId = AircraftModelId.Create(id);

        var aircraftModel = await _getByIdUseCase.ExecuteAsync(aircraftModelId);
        if (aircraftModel != null)
        {
            Console.WriteLine($"ID: {aircraftModel.Id.Value}, Manufacturer ID: {aircraftModel.ManufacturerId.Value}, Model: {aircraftModel.ModelName.Value}, Capacity: {aircraftModel.MaxCapacity.Value}, MTOW: {aircraftModel.MaxTakeoffWeightKg?.Value ?? 0}, Fuel: {aircraftModel.FuelConsumptionKgH?.Value ?? 0}, Speed: {aircraftModel.CruisingSpeedKmh?.Value ?? 0}, Altitude: {aircraftModel.CruisingAltitudeFt?.Value ?? 0}");
        }
        else
        {
            Console.WriteLine("Aircraft model not found.");
        }
    }

    private async Task UpdateAircraftModelAsync()
    {
        try
        {
            Console.Write("Enter Aircraft Model ID (int): ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftModelId = AircraftModelId.Create(id);

            Console.Write("Enter new Manufacturer ID (int): ");
            var manufacturerIdValue = int.Parse(Console.ReadLine()!);
            var manufacturerId = ManufacturerId.Create(manufacturerIdValue);

            Console.Write("Enter new Model Name: ");
            var modelName = Console.ReadLine()!;
            var modelNameValue = ModelName.Create(modelName);

            Console.Write("Enter new Max Capacity (int): ");
            var maxCapacityValue = int.Parse(Console.ReadLine()!);
            var maxCapacity = MaxCapacity.Create(maxCapacityValue);

            Console.Write("Enter new Max Takeoff Weight Kg (decimal, optional): ");
            var maxTakeoffWeightInput = Console.ReadLine();
            MaxTakeoffWeightKg? maxTakeoffWeightKg = null;
            if (!string.IsNullOrWhiteSpace(maxTakeoffWeightInput))
            {
                var maxTakeoffWeightValue = decimal.Parse(maxTakeoffWeightInput);
                maxTakeoffWeightKg = MaxTakeoffWeightKg.Create(maxTakeoffWeightValue);
            }

            Console.Write("Enter new Fuel Consumption Kg/H (decimal, optional): ");
            var fuelConsumptionInput = Console.ReadLine();
            FuelConsumptionKgH? fuelConsumptionKgH = null;
            if (!string.IsNullOrWhiteSpace(fuelConsumptionInput))
            {
                var fuelConsumptionValue = decimal.Parse(fuelConsumptionInput);
                fuelConsumptionKgH = FuelConsumptionKgH.Create(fuelConsumptionValue);
            }

            Console.Write("Enter new Cruising Speed Kmh (int, optional): ");
            var cruisingSpeedInput = Console.ReadLine();
            CruisingSpeedKmh? cruisingSpeedKmh = null;
            if (!string.IsNullOrWhiteSpace(cruisingSpeedInput))
            {
                var cruisingSpeedValue = int.Parse(cruisingSpeedInput);
                cruisingSpeedKmh = CruisingSpeedKmh.Create(cruisingSpeedValue);
            }

            Console.Write("Enter new Cruising Altitude Ft (int, optional): ");
            var cruisingAltitudeInput = Console.ReadLine();
            CruisingAltitudeFt? cruisingAltitudeFt = null;
            if (!string.IsNullOrWhiteSpace(cruisingAltitudeInput))
            {
                var cruisingAltitudeValue = int.Parse(cruisingAltitudeInput);
                cruisingAltitudeFt = CruisingAltitudeFt.Create(cruisingAltitudeValue);
            }

            await _updateUseCase.ExecuteAsync(aircraftModelId, manufacturerId, modelNameValue, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
            Console.WriteLine("Aircraft model updated successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteAircraftModelAsync()
    {
        Console.Write("Enter Aircraft Model ID (int): ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftModelId = AircraftModelId.Create(id);

        await _deleteUseCase.ExecuteAsync(aircraftModelId);
        Console.WriteLine("Aircraft model deleted successfully.");
    }
}
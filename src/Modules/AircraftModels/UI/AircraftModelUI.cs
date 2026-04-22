using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.UI;

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
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Modelos de aeronave", "Especificaciones técnicas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear modelo", () => CreateAircraftModelAsync().GetAwaiter().GetResult()),
                ("Listar todos", () => ViewAllAircraftModelsAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewAircraftModelByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateAircraftModelAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteAircraftModelAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAircraftModelAsync()
    {
        SpectreUi.ModuleHeader("Crear modelo de aeronave", null);
        try
        {
            Console.Write("ID del modelo (entero): ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftModelId = AircraftModelId.Create(id);

            Console.Write("ID del fabricante: ");
            var manufacturerIdValue = int.Parse(Console.ReadLine()!);
            var manufacturerId = ManufacturerId.Create(manufacturerIdValue);

            Console.Write("Nombre del modelo: ");
            var modelName = Console.ReadLine()!;
            var modelNameValue = ModelName.Create(modelName);

            Console.Write("Capacidad máxima (pasajeros): ");
            var maxCapacityValue = int.Parse(Console.ReadLine()!);
            var maxCapacity = MaxCapacity.Create(maxCapacityValue);

            Console.Write("Peso máximo al despegue (kg, opcional): ");
            var maxTakeoffWeightInput = Console.ReadLine();
            MaxTakeoffWeightKg? maxTakeoffWeightKg = null;
            if (!string.IsNullOrWhiteSpace(maxTakeoffWeightInput))
            {
                var maxTakeoffWeightValue = decimal.Parse(maxTakeoffWeightInput);
                maxTakeoffWeightKg = MaxTakeoffWeightKg.Create(maxTakeoffWeightValue);
            }

            Console.Write("Consumo combustible (kg/h, opcional): ");
            var fuelConsumptionInput = Console.ReadLine();
            FuelConsumptionKgH? fuelConsumptionKgH = null;
            if (!string.IsNullOrWhiteSpace(fuelConsumptionInput))
            {
                var fuelConsumptionValue = decimal.Parse(fuelConsumptionInput);
                fuelConsumptionKgH = FuelConsumptionKgH.Create(fuelConsumptionValue);
            }

            Console.Write("Velocidad crucero (km/h, opcional): ");
            var cruisingSpeedInput = Console.ReadLine();
            CruisingSpeedKmh? cruisingSpeedKmh = null;
            if (!string.IsNullOrWhiteSpace(cruisingSpeedInput))
            {
                var cruisingSpeedValue = int.Parse(cruisingSpeedInput);
                cruisingSpeedKmh = CruisingSpeedKmh.Create(cruisingSpeedValue);
            }

            Console.Write("Altitud crucero (pies, opcional): ");
            var cruisingAltitudeInput = Console.ReadLine();
            CruisingAltitudeFt? cruisingAltitudeFt = null;
            if (!string.IsNullOrWhiteSpace(cruisingAltitudeInput))
            {
                var cruisingAltitudeValue = int.Parse(cruisingAltitudeInput);
                cruisingAltitudeFt = CruisingAltitudeFt.Create(cruisingAltitudeValue);
            }

            await _createUseCase.ExecuteAsync(aircraftModelId, manufacturerId, modelNameValue, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
            Console.WriteLine("Modelo creado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllAircraftModelsAsync()
    {
        SpectreUi.ModuleHeader("Modelos registrados", null);
        var aircraftModels = await _getAllUseCase.ExecuteAsync();
        foreach (var am in aircraftModels)
        {
            Console.WriteLine($"ID: {am.Id.Value}, Fabricante: {am.ManufacturerId.Value}, Modelo: {am.ModelName.Value}, Cap.: {am.MaxCapacity.Value}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAircraftModelByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar modelo", null);
        Console.Write("ID del modelo: ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftModelId = AircraftModelId.Create(id);

        var aircraftModel = await _getByIdUseCase.ExecuteAsync(aircraftModelId);
        if (aircraftModel != null)
        {
            Console.WriteLine($"ID: {aircraftModel.Id.Value}, Fabricante: {aircraftModel.ManufacturerId.Value}, Modelo: {aircraftModel.ModelName.Value}, Cap.: {aircraftModel.MaxCapacity.Value}");
        }
        else
        {
            Console.WriteLine("Modelo no encontrado.");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateAircraftModelAsync()
    {
        SpectreUi.ModuleHeader("Actualizar modelo", null);
        try
        {
            Console.Write("ID del modelo: ");
            var id = int.Parse(Console.ReadLine()!);
            var aircraftModelId = AircraftModelId.Create(id);

            Console.Write("Nuevo ID de fabricante: ");
            var manufacturerIdValue = int.Parse(Console.ReadLine()!);
            var manufacturerId = ManufacturerId.Create(manufacturerIdValue);

            Console.Write("Nuevo nombre del modelo: ");
            var modelName = Console.ReadLine()!;
            var modelNameValue = ModelName.Create(modelName);

            Console.Write("Nueva capacidad máxima: ");
            var maxCapacityValue = int.Parse(Console.ReadLine()!);
            var maxCapacity = MaxCapacity.Create(maxCapacityValue);

            Console.Write("Nuevo MTOW kg (opcional): ");
            var maxTakeoffWeightInput = Console.ReadLine();
            MaxTakeoffWeightKg? maxTakeoffWeightKg = null;
            if (!string.IsNullOrWhiteSpace(maxTakeoffWeightInput))
            {
                var maxTakeoffWeightValue = decimal.Parse(maxTakeoffWeightInput);
                maxTakeoffWeightKg = MaxTakeoffWeightKg.Create(maxTakeoffWeightValue);
            }

            Console.Write("Nuevo consumo kg/h (opcional): ");
            var fuelConsumptionInput = Console.ReadLine();
            FuelConsumptionKgH? fuelConsumptionKgH = null;
            if (!string.IsNullOrWhiteSpace(fuelConsumptionInput))
            {
                var fuelConsumptionValue = decimal.Parse(fuelConsumptionInput);
                fuelConsumptionKgH = FuelConsumptionKgH.Create(fuelConsumptionValue);
            }

            Console.Write("Nueva velocidad crucero km/h (opcional): ");
            var cruisingSpeedInput = Console.ReadLine();
            CruisingSpeedKmh? cruisingSpeedKmh = null;
            if (!string.IsNullOrWhiteSpace(cruisingSpeedInput))
            {
                var cruisingSpeedValue = int.Parse(cruisingSpeedInput);
                cruisingSpeedKmh = CruisingSpeedKmh.Create(cruisingSpeedValue);
            }

            Console.Write("Nueva altitud crucero pies (opcional): ");
            var cruisingAltitudeInput = Console.ReadLine();
            CruisingAltitudeFt? cruisingAltitudeFt = null;
            if (!string.IsNullOrWhiteSpace(cruisingAltitudeInput))
            {
                var cruisingAltitudeValue = int.Parse(cruisingAltitudeInput);
                cruisingAltitudeFt = CruisingAltitudeFt.Create(cruisingAltitudeValue);
            }

            await _updateUseCase.ExecuteAsync(aircraftModelId, manufacturerId, modelNameValue, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
            Console.WriteLine("Modelo actualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteAircraftModelAsync()
    {
        SpectreUi.ModuleHeader("Eliminar modelo", null);
        Console.Write("ID del modelo: ");
        var id = int.Parse(Console.ReadLine()!);
        var aircraftModelId = AircraftModelId.Create(id);

        await _deleteUseCase.ExecuteAsync(aircraftModelId);
        Console.WriteLine("Modelo eliminado.");
        SpectreUi.Pause();
    }
}

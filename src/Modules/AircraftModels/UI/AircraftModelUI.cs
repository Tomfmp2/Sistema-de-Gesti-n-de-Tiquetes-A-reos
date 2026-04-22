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
                ("Crear", () => CreateAircraftModelAsync().GetAwaiter().GetResult()),
                ("Listar", () => ViewAllAircraftModelsAsync().GetAwaiter().GetResult()),
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
        try
        {
            SpectreUi.ModuleHeader("Modelos de aeronave", "Crear");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del modelo", "0/c/cancelar para salir", min: 1);
            var aircraftModelId = AircraftModelId.Create(id);

            var manufacturerIdValue = SpectreUi.PromptIntRequiredCancelable("ID del fabricante", "0/c/cancelar para salir", min: 1);
            var manufacturerId = ManufacturerId.Create(manufacturerIdValue);

            var modelName = SpectreUi.PromptRequiredCancelable("Nombre del modelo", "0/c/cancelar para salir");
            var modelNameValue = ModelName.Create(modelName);

            var maxCapacityValue = SpectreUi.PromptIntRequiredCancelable("Capacidad máxima (pasajeros)", "0/c/cancelar para salir", min: 1);
            var maxCapacity = MaxCapacity.Create(maxCapacityValue);

            var maxTakeoffWeightInput = SpectreUi.PromptOptionalCancelable("Peso máximo al despegue (kg)", "opcional");
            MaxTakeoffWeightKg? maxTakeoffWeightKg = null;
            if (!string.IsNullOrWhiteSpace(maxTakeoffWeightInput))
            {
                var maxTakeoffWeightValue = decimal.Parse(maxTakeoffWeightInput);
                maxTakeoffWeightKg = MaxTakeoffWeightKg.Create(maxTakeoffWeightValue);
            }

            var fuelConsumptionInput = SpectreUi.PromptOptionalCancelable("Consumo combustible (kg/h)", "opcional");
            FuelConsumptionKgH? fuelConsumptionKgH = null;
            if (!string.IsNullOrWhiteSpace(fuelConsumptionInput))
            {
                var fuelConsumptionValue = decimal.Parse(fuelConsumptionInput);
                fuelConsumptionKgH = FuelConsumptionKgH.Create(fuelConsumptionValue);
            }

            var cruisingSpeedInput = SpectreUi.PromptOptionalCancelable("Velocidad crucero (km/h)", "opcional");
            CruisingSpeedKmh? cruisingSpeedKmh = null;
            if (!string.IsNullOrWhiteSpace(cruisingSpeedInput))
            {
                var cruisingSpeedValue = int.Parse(cruisingSpeedInput);
                cruisingSpeedKmh = CruisingSpeedKmh.Create(cruisingSpeedValue);
            }

            var cruisingAltitudeInput = SpectreUi.PromptOptionalCancelable("Altitud crucero (pies)", "opcional");
            CruisingAltitudeFt? cruisingAltitudeFt = null;
            if (!string.IsNullOrWhiteSpace(cruisingAltitudeInput))
            {
                var cruisingAltitudeValue = int.Parse(cruisingAltitudeInput);
                cruisingAltitudeFt = CruisingAltitudeFt.Create(cruisingAltitudeValue);
            }

            await _createUseCase.ExecuteAsync(aircraftModelId, manufacturerId, modelNameValue, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
            SpectreUi.MarkupLineOrPlain("[green]Modelo creado correctamente.[/]", "Modelo creado correctamente.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllAircraftModelsAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Modelos de aeronave", "Listar");
            var aircraftModels = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (aircraftModels.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay modelos registrados.[/]", "No hay modelos registrados.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Modelos",
                ["ID", "FabricanteId", "Modelo", "Capacidad"],
                aircraftModels
                    .OrderBy(x => x.Id.Value)
                    .Select(am => (IReadOnlyList<string>)
                    [
                        am.Id.Value.ToString(),
                        am.ManufacturerId.Value.ToString(),
                        am.ModelName.Value,
                        am.MaxCapacity.Value.ToString()
                    ])
                    .ToList()
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }
        SpectreUi.Pause();
    }

    private async Task ViewAircraftModelByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Modelos de aeronave", "Consultar por ID");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del modelo", "0/c/cancelar para salir", min: 1);
            var aircraftModelId = AircraftModelId.Create(id);

            var aircraftModel = await _getByIdUseCase.ExecuteAsync(aircraftModelId);
            if (aircraftModel is null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Modelo no encontrado.[/]", "Modelo no encontrado.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Modelo",
                ["Campo", "Valor"],
                [
                    ["ID", aircraftModel.Id.Value.ToString()],
                    ["FabricanteId", aircraftModel.ManufacturerId.Value.ToString()],
                    ["Nombre", aircraftModel.ModelName.Value],
                    ["Capacidad", aircraftModel.MaxCapacity.Value.ToString()],
                    ["MTOW (kg)", aircraftModel.MaxTakeoffWeightKg?.Value.ToString("0.##") ?? "-"],
                    ["Consumo (kg/h)", aircraftModel.FuelConsumptionKgH?.Value.ToString("0.##") ?? "-"],
                    ["Velocidad (km/h)", aircraftModel.CruisingSpeedKmh?.Value.ToString() ?? "-"],
                    ["Altitud (ft)", aircraftModel.CruisingAltitudeFt?.Value.ToString() ?? "-"]
                ]
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }
        SpectreUi.Pause();
    }

    private async Task UpdateAircraftModelAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Modelos de aeronave", "Actualizar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del modelo", "0/c/cancelar para salir", min: 1);
            var aircraftModelId = AircraftModelId.Create(id);

            var manufacturerIdValue = SpectreUi.PromptIntRequiredCancelable("Nuevo ID de fabricante", "0/c/cancelar para salir", min: 1);
            var manufacturerId = ManufacturerId.Create(manufacturerIdValue);

            var modelName = SpectreUi.PromptRequiredCancelable("Nuevo nombre del modelo", "0/c/cancelar para salir");
            var modelNameValue = ModelName.Create(modelName);

            var maxCapacityValue = SpectreUi.PromptIntRequiredCancelable("Nueva capacidad máxima", "0/c/cancelar para salir", min: 1);
            var maxCapacity = MaxCapacity.Create(maxCapacityValue);

            var maxTakeoffWeightInput = SpectreUi.PromptOptionalCancelable("Nuevo MTOW (kg)", "opcional");
            MaxTakeoffWeightKg? maxTakeoffWeightKg = null;
            if (!string.IsNullOrWhiteSpace(maxTakeoffWeightInput))
            {
                var maxTakeoffWeightValue = decimal.Parse(maxTakeoffWeightInput);
                maxTakeoffWeightKg = MaxTakeoffWeightKg.Create(maxTakeoffWeightValue);
            }

            var fuelConsumptionInput = SpectreUi.PromptOptionalCancelable("Nuevo consumo (kg/h)", "opcional");
            FuelConsumptionKgH? fuelConsumptionKgH = null;
            if (!string.IsNullOrWhiteSpace(fuelConsumptionInput))
            {
                var fuelConsumptionValue = decimal.Parse(fuelConsumptionInput);
                fuelConsumptionKgH = FuelConsumptionKgH.Create(fuelConsumptionValue);
            }

            var cruisingSpeedInput = SpectreUi.PromptOptionalCancelable("Nueva velocidad crucero (km/h)", "opcional");
            CruisingSpeedKmh? cruisingSpeedKmh = null;
            if (!string.IsNullOrWhiteSpace(cruisingSpeedInput))
            {
                var cruisingSpeedValue = int.Parse(cruisingSpeedInput);
                cruisingSpeedKmh = CruisingSpeedKmh.Create(cruisingSpeedValue);
            }

            var cruisingAltitudeInput = SpectreUi.PromptOptionalCancelable("Nueva altitud crucero (ft)", "opcional");
            CruisingAltitudeFt? cruisingAltitudeFt = null;
            if (!string.IsNullOrWhiteSpace(cruisingAltitudeInput))
            {
                var cruisingAltitudeValue = int.Parse(cruisingAltitudeInput);
                cruisingAltitudeFt = CruisingAltitudeFt.Create(cruisingAltitudeValue);
            }

            await _updateUseCase.ExecuteAsync(aircraftModelId, manufacturerId, modelNameValue, maxCapacity, maxTakeoffWeightKg, fuelConsumptionKgH, cruisingSpeedKmh, cruisingAltitudeFt);
            SpectreUi.MarkupLineOrPlain("[green]Modelo actualizado.[/]", "Modelo actualizado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }

        SpectreUi.Pause();
    }

    private async Task DeleteAircraftModelAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Modelos de aeronave", "Eliminar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del modelo", "0/c/cancelar para salir", min: 1);
            var confirm = SpectreUi.PromptBool("¿Confirma la eliminación?", defaultValue: false);
            if (!confirm)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Eliminación cancelada.[/]", "Eliminación cancelada.");
                SpectreUi.Pause();
                return;
            }

            var aircraftModelId = AircraftModelId.Create(id);
            await _deleteUseCase.ExecuteAsync(aircraftModelId);
            SpectreUi.MarkupLineOrPlain("[green]Modelo eliminado.[/]", "Modelo eliminado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ExceptionFormatting.GetDiagnosticMessage(ex)}",
                $"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}"
            );
        }
        SpectreUi.Pause();
    }
}

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
                ("Crear", () => CreateAircraftManufacturerAsync().GetAwaiter().GetResult()),
                ("Listar", () => ViewAllAircraftManufacturersAsync().GetAwaiter().GetResult()),
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
        try
        {
            SpectreUi.ModuleHeader("Fabricantes de aeronaves", "Crear");

            var id = SpectreUi.PromptIntRequiredCancelable("ID del fabricante", "0/c/cancelar para salir", min: 1);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            var name = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir");
            var aircraftManufacturerName = AircraftManufacturerName.Create(name);

            var country = SpectreUi.PromptRequiredCancelable("País", "0/c/cancelar para salir");
            var countryValue = Country.Create(country);

            await _createUseCase.ExecuteAsync(aircraftManufacturerId, aircraftManufacturerName, countryValue);
            SpectreUi.MarkupLineOrPlain("[green]Fabricante creado correctamente.[/]", "Fabricante creado correctamente.");
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

    private async Task ViewAllAircraftManufacturersAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Fabricantes de aeronaves", "Listar");
            var aircraftManufacturers = (await _getAllUseCase.ExecuteAsync()).ToList();
            if (aircraftManufacturers.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay fabricantes registrados.[/]", "No hay fabricantes registrados.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Fabricantes",
                ["ID", "Nombre", "País"],
                aircraftManufacturers
                    .OrderBy(x => x.Id.Value)
                    .Select(am => (IReadOnlyList<string>)
                    [
                        am.Id.Value.ToString(),
                        am.Name.Value,
                        am.Country.Value
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

    private async Task ViewAircraftManufacturerByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Fabricantes de aeronaves", "Consultar por ID");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del fabricante", "0/c/cancelar para salir", min: 1);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            var aircraftManufacturer = await _getByIdUseCase.ExecuteAsync(aircraftManufacturerId);
            if (aircraftManufacturer is null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No se encontró el fabricante.[/]", "No se encontró el fabricante.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Fabricante",
                ["Campo", "Valor"],
                [
                    ["ID", aircraftManufacturer.Id.Value.ToString()],
                    ["Nombre", aircraftManufacturer.Name.Value],
                    ["País", aircraftManufacturer.Country.Value]
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

    private async Task UpdateAircraftManufacturerAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Fabricantes de aeronaves", "Actualizar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del fabricante", "0/c/cancelar para salir", min: 1);
            var aircraftManufacturerId = AircraftManufacturerId.Create(id);

            var name = SpectreUi.PromptRequiredCancelable("Nuevo nombre", "0/c/cancelar para salir");
            var aircraftManufacturerName = AircraftManufacturerName.Create(name);

            var country = SpectreUi.PromptRequiredCancelable("Nuevo país", "0/c/cancelar para salir");
            var countryValue = Country.Create(country);

            await _updateUseCase.ExecuteAsync(aircraftManufacturerId, aircraftManufacturerName, countryValue);
            SpectreUi.MarkupLineOrPlain("[green]Fabricante actualizado.[/]", "Fabricante actualizado.");
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

    private async Task DeleteAircraftManufacturerAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Fabricantes de aeronaves", "Eliminar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID del fabricante", "0/c/cancelar para salir", min: 1);
            var confirm = SpectreUi.PromptBool("¿Confirma la eliminación?", defaultValue: false);
            if (!confirm)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Eliminación cancelada.[/]", "Eliminación cancelada.");
                SpectreUi.Pause();
                return;
            }

            var aircraftManufacturerId = AircraftManufacturerId.Create(id);
            await _deleteUseCase.ExecuteAsync(aircraftManufacturerId);
            SpectreUi.MarkupLineOrPlain("[green]Fabricante eliminado.[/]", "Fabricante eliminado.");
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

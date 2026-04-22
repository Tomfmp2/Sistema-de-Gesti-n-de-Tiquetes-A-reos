namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.UI;

using Application.UseCases;
using Shared.Helpers;
using Shared.Ui;

public class BaggageTypeConsoleUI : IModuleUI
{
    private readonly CreateBaggageTypeUseCase _createUseCase;
    private readonly GetBaggageTypeByIdUseCase _getByIdUseCase;
    private readonly GetAllBaggageTypesUseCase _getAllUseCase;
    private readonly UpdateBaggageTypeUseCase _updateUseCase;
    private readonly DeleteBaggageTypeUseCase _deleteUseCase;

    public BaggageTypeConsoleUI(
        CreateBaggageTypeUseCase createUseCase,
        GetBaggageTypeByIdUseCase getByIdUseCase,
        GetAllBaggageTypesUseCase getAllUseCase,
        UpdateBaggageTypeUseCase updateUseCase,
        DeleteBaggageTypeUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getByIdUseCase = getByIdUseCase;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Tipos de equipaje", "Crear / listar / consultar / actualizar / eliminar");

            var items = new List<(string Label, Action Action)>
            {
                ("Crear", () => CreateBaggageTypeAsync().GetAwaiter().GetResult()),
                ("Listar", () => ListAllBaggageTypesAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetBaggageTypeByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateBaggageTypeAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteBaggageTypeAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateBaggageTypeAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de equipaje", "Crear");
            var name = SpectreUi.PromptRequiredCancelable("Nombre", "0/c/cancelar para salir");
            var maxWeight = decimal.Parse(SpectreUi.PromptRequiredCancelable("Peso máximo (kg)", "0/c/cancelar para salir"));
            var basePrice = decimal.Parse(SpectreUi.PromptRequiredCancelable("Precio base", "0/c/cancelar para salir"));

            var baggageType = await _createUseCase.ExecuteAsync(name, maxWeight, basePrice);
            SpectreUi.MarkupLineOrPlain(
                $"[green]Creado[/] id={baggageType.Id.Value}.",
                $"Creado id={baggageType.Id.Value}."
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

    private async Task GetBaggageTypeByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de equipaje", "Consultar por ID");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", "0/c/cancelar para salir", min: 1);
            var baggageType = await _getByIdUseCase.ExecuteAsync(id);

            SpectreUi.ShowTable(
                "Tipo de equipaje",
                ["Campo", "Valor"],
                [
                    ["ID", baggageType.Id.Value.ToString()],
                    ["Nombre", baggageType.Name.Value],
                    ["Peso máx (kg)", baggageType.MaxWeightKg.Value.ToString("0.##")],
                    ["Precio base", baggageType.BasePrice.Value.ToString("0.00")]
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

    private async Task ListAllBaggageTypesAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de equipaje", "Listar");
            var baggageTypes = await _getAllUseCase.ExecuteAsync();
            if (baggageTypes.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay tipos de equipaje registrados.[/]", "No hay tipos de equipaje registrados.");
                return;
            }

            SpectreUi.ShowTable(
                "Tipos de equipaje",
                ["ID", "Nombre", "Peso máx(kg)", "Precio base"],
                baggageTypes.Select(bt => (IReadOnlyList<string>)
                [
                    bt.Id.Value.ToString(),
                    bt.Name.Value,
                    bt.MaxWeightKg.Value.ToString("0.##"),
                    bt.BasePrice.Value.ToString("0.00")
                ]).ToList()
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

    private async Task UpdateBaggageTypeAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de equipaje", "Actualizar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", "0/c/cancelar para salir", min: 1);
            var newName = SpectreUi.PromptOptionalCancelable("Nuevo nombre", "Enter=omitir");
            if (string.IsNullOrWhiteSpace(newName))
                newName = null;

            var newMaxWeightRaw = SpectreUi.PromptOptionalCancelable("Nuevo peso máximo (kg)", "Enter=omitir");
            decimal? newMaxWeight = string.IsNullOrWhiteSpace(newMaxWeightRaw) ? null : decimal.Parse(newMaxWeightRaw);

            var newBasePriceRaw = SpectreUi.PromptOptionalCancelable("Nuevo precio base", "Enter=omitir");
            decimal? newBasePrice = string.IsNullOrWhiteSpace(newBasePriceRaw) ? null : decimal.Parse(newBasePriceRaw);

            await _updateUseCase.ExecuteAsync(id, newName, newMaxWeight, newBasePrice);
            SpectreUi.MarkupLineOrPlain("[green]Actualizado correctamente.[/]", "Actualizado correctamente.");
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

    private async Task DeleteBaggageTypeAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Tipos de equipaje", "Eliminar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", "0/c/cancelar para salir", min: 1);
            var confirm = SpectreUi.PromptBool("¿Confirma la eliminación?", defaultValue: false);
            if (!confirm)
            {
                SpectreUi.MarkupLineOrPlain("[grey]Eliminación cancelada.[/]", "Eliminación cancelada.");
                return;
            }

            await _deleteUseCase.ExecuteAsync(id);
            SpectreUi.MarkupLineOrPlain("[green]Eliminado correctamente.[/]", "Eliminado correctamente.");
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

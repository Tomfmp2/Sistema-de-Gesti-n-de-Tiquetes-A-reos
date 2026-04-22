using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.UI;

public sealed class BaggageConsoleUI : IModuleUI
{
    private readonly CreateBaggageUseCase _createUseCase;
    private readonly GetBaggageByIdUseCase _getByIdUseCase;
    private readonly GetAllBaggagesUseCase _getAllUseCase;
    private readonly UpdateBaggageUseCase _updateUseCase;
    private readonly DeleteBaggageUseCase _deleteUseCase;

    public BaggageConsoleUI(
        CreateBaggageUseCase createUseCase,
        GetBaggageByIdUseCase getByIdUseCase,
        GetAllBaggagesUseCase getAllUseCase,
        UpdateBaggageUseCase updateUseCase,
        DeleteBaggageUseCase deleteUseCase)
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
            SpectreUi.ModuleHeader("Equipaje", "Crear / listar / consultar / actualizar / eliminar");

            var items = new List<(string Label, Action Action)>
            {
                ("Crear", () => CreateAsync().GetAwaiter().GetResult()),
                ("Listar", () => ListAllAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Equipaje", "Crear");
            var checkinId = SpectreUi.PromptIntRequiredCancelable("ID check-in", "0/c/cancelar para salir", min: 1);
            var baggageTypeId = SpectreUi.PromptIntRequiredCancelable("ID tipo de equipaje", "0/c/cancelar para salir", min: 1);
            var weight = decimal.Parse(SpectreUi.PromptRequiredCancelable("Peso (kg)", "0/c/cancelar para salir"));
            var price = decimal.Parse(SpectreUi.PromptRequiredCancelable("Precio cobrado", "0/c/cancelar para salir"));

            await _createUseCase.ExecuteAsync(checkinId, baggageTypeId, weight, price);
            SpectreUi.MarkupLineOrPlain("[green]Equipaje registrado correctamente.[/]", "Equipaje registrado correctamente.");
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

    private async Task GetByIdAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Equipaje", "Consultar por ID");
            var id = SpectreUi.PromptIntRequiredCancelable("ID equipaje", "0/c/cancelar para salir", min: 1);

            var baggage = await _getByIdUseCase.ExecuteAsync(id);

            SpectreUi.ShowTable(
                "Equipaje",
                ["Campo", "Valor"],
                [
                    ["ID", baggage.Id.Value.ToString()],
                    ["Check-in", baggage.CheckinId.ToString()],
                    ["Tipo equipaje", baggage.BaggageTypeId.ToString()],
                    ["Peso (kg)", baggage.WeightKg.Value.ToString("0.##")],
                    ["Precio cobrado", baggage.ChargedPrice.Value.ToString("0.00")],
                    ["Creado", baggage.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")],
                    ["Actualizado", baggage.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")]
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

    private async Task ListAllAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Equipaje", "Listar");
            var baggages = await _getAllUseCase.ExecuteAsync();

            if (baggages.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay equipajes registrados.[/]", "No hay equipajes registrados.");
                return;
            }

            SpectreUi.ShowTable(
                "Equipajes",
                ["ID", "Check-in", "Tipo", "Peso(kg)", "Precio"],
                baggages.Select(b => (IReadOnlyList<string>)
                [
                    b.Id.Value.ToString(),
                    b.CheckinId.ToString(),
                    b.BaggageTypeId.ToString(),
                    b.WeightKg.Value.ToString("0.##"),
                    b.ChargedPrice.Value.ToString("0.00")
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

    private async Task UpdateAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Equipaje", "Actualizar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID equipaje", "0/c/cancelar para salir", min: 1);

            var baggageTypeIdRaw = SpectreUi.PromptOptionalCancelable("Nuevo ID tipo de equipaje", "Enter=omitir");
            int? baggageTypeId = string.IsNullOrWhiteSpace(baggageTypeIdRaw) ? null : int.Parse(baggageTypeIdRaw);

            var weightRaw = SpectreUi.PromptOptionalCancelable("Nuevo peso (kg)", "Enter=omitir");
            decimal? weight = string.IsNullOrWhiteSpace(weightRaw) ? null : decimal.Parse(weightRaw);

            var priceRaw = SpectreUi.PromptOptionalCancelable("Nuevo precio cobrado", "Enter=omitir");
            decimal? price = string.IsNullOrWhiteSpace(priceRaw) ? null : decimal.Parse(priceRaw);

            await _updateUseCase.ExecuteAsync(id, baggageTypeId, weight, price);
            SpectreUi.MarkupLineOrPlain("[green]Equipaje actualizado correctamente.[/]", "Equipaje actualizado correctamente.");
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

    private async Task DeleteAsync()
    {
        try
        {
            SpectreUi.ModuleHeader("Equipaje", "Eliminar");
            var id = SpectreUi.PromptIntRequiredCancelable("ID equipaje", "0/c/cancelar para salir", min: 1);

            await _deleteUseCase.ExecuteAsync(id);
            SpectreUi.MarkupLineOrPlain("[green]Equipaje eliminado correctamente.[/]", "Equipaje eliminado correctamente.");
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

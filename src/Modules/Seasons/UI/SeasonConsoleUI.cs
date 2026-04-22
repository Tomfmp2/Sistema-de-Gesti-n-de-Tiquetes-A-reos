using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.UI;

public class SeasonConsoleUI : IModuleUI
{
    private readonly CreateSeasonUseCase _createUseCase;
    private readonly GetSeasonByIdUseCase _getByIdUseCase;
    private readonly GetAllSeasonsUseCase _getAllUseCase;
    private readonly UpdateSeasonUseCase _updateUseCase;
    private readonly DeleteSeasonUseCase _deleteUseCase;

    public SeasonConsoleUI(CreateSeasonUseCase createUseCase, GetSeasonByIdUseCase getByIdUseCase, GetAllSeasonsUseCase getAllUseCase, UpdateSeasonUseCase updateUseCase, DeleteSeasonUseCase deleteUseCase)
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
            SpectreUi.ModuleHeader("Temporadas", "Gestión de temporadas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear temporada", () => CreateSeason().GetAwaiter().GetResult()),
                ("Listar todas", () => GetAllSeasons().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetSeasonById().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateSeason().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteSeason().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateSeason()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear temporada", "Nombre y factor");
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            var description = SpectreUi.PromptOptionalCancelable("Descripción", "opcional");
            var priceFactor = PromptDecimalRequired("Factor de precio", "p.ej. 1.10");
            await _createUseCase.ExecuteAsync(
                SeasonName.Create(name),
                SeasonDescription.Create(description),
                PriceFactor.Create(priceFactor)
            );
            SpectreUi.MarkupLineOrPlain("[green]Temporada creada.[/]", "Temporada creada.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task GetSeasonById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar temporada", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID temporada", min: 1);
            var season = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
            if (season != null)
            {
                SpectreUi.ShowTable(
                    "Temporada",
                    ["Campo", "Valor"],
                    [
                        ["ID", season.Id.Value.ToString()],
                        ["Nombre", season.Name.Value],
                        ["Descripción", season.Description.Value ?? ""],
                        ["Factor", season.PriceFactor.Value.ToString()],
                    ]
                );
            }
            else
            {
                Console.WriteLine("Temporada no encontrada");
            }
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task GetAllSeasons()
    {
        try
        {
            var seasons = await _getAllUseCase.ExecuteAsync();
            var list = seasons.ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("No hay temporadas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Temporadas", "Listado");
            SpectreUi.ShowTable(
                "Temporadas",
                ["ID", "Nombre", "Factor", "Descripción"],
                list
                    .OrderBy(s => s.Id.Value)
                    .Select(s => (IReadOnlyList<string>)new[]
                    {
                        s.Id.Value.ToString(),
                        s.Name.Value,
                        s.PriceFactor.Value.ToString(),
                        s.Description.Value ?? ""
                    })
                    .ToList()
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task UpdateSeason()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar temporada", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID temporada", min: 1);
            var existing = await _getByIdUseCase.ExecuteAsync(SeasonId.Create(id));
            if (existing == null)
            {
                Console.WriteLine("Temporada no encontrada");
                SpectreUi.Pause();
                return;
            }

            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            var description = SpectreUi.PromptOptionalCancelable("Descripción", "opcional");
            var priceFactor = PromptDecimalRequired("Factor de precio", "p.ej. 1.10");
            await _updateUseCase.ExecuteAsync(
                SeasonId.Create(id),
                SeasonName.Create(name),
                SeasonDescription.Create(description),
                PriceFactor.Create(priceFactor)
            );
            SpectreUi.MarkupLineOrPlain("[green]Temporada actualizada.[/]", "Temporada actualizada.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private async Task DeleteSeason()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar temporada", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID temporada", min: 1);
            await _deleteUseCase.ExecuteAsync(SeasonId.Create(id));
            SpectreUi.MarkupLineOrPlain("[green]Temporada eliminada.[/]", "Temporada eliminada.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private static decimal PromptDecimalRequired(string label, string? help = null)
    {
        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(label, help);
            if (decimal.TryParse(raw, out var value))
                return value;

            SpectreUi.MarkupLineOrPlain("[red]Número decimal inválido.[/]", "Número decimal inválido.");
        }
    }
}
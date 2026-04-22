using sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.AirportAirline.UI;

public class AirportAirlineConsoleUI : IModuleUI
{
    private readonly CreateAirportAirlineUseCase _createUseCase;
    private readonly GetAirportAirlineByIdUseCase _getByIdUseCase;
    private readonly GetAllAirportAirlinesUseCase _getAllUseCase;
    private readonly UpdateAirportAirlineUseCase _updateUseCase;
    private readonly DeleteAirportAirlineUseCase _deleteUseCase;

    public AirportAirlineConsoleUI(CreateAirportAirlineUseCase create, GetAirportAirlineByIdUseCase getById, GetAllAirportAirlinesUseCase getAll, UpdateAirportAirlineUseCase update, DeleteAirportAirlineUseCase delete)
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
            SpectreUi.ModuleHeader("Aeropuerto ↔ Aerolínea", "Operación por terminal y fechas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear relación", CreateAirportAirline),
                ("Listar todas", GetAllAirportAirlines),
                ("Consultar por ID", GetAirportAirlineById),
                ("Actualizar", UpdateAirportAirline),
                ("Eliminar", DeleteAirportAirline),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private void CreateAirportAirline()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear relación", "Aeropuerto ↔ Aerolínea");
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var airportId = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto", min: 1);
            var airlineId = SpectreUi.PromptIntRequiredCancelable("ID aerolínea", min: 1);
            var terminal = SpectreUi.PromptOptionalCancelable("Terminal", "opcional");

            var startDate = PromptDateOnlyRequired("Fecha inicio", "yyyy-MM-dd");
            var endDate = PromptDateOnlyOptional("Fecha fin", "yyyy-MM-dd (opcional)");

            var airportAirline = _createUseCase
                .ExecuteAsync(
                    airportId,
                    airlineId,
                    string.IsNullOrWhiteSpace(terminal) ? null : terminal,
                    startDate,
                    endDate
                )
                .Result;

            SpectreUi.MarkupLineOrPlain(
                $"[green]Relación creada[/] id={airportAirline.Id.Value} aeropuerto={airportAirline.AirportId} aerolínea={airportAirline.AirlineId}.",
                $"Relación creada id={airportAirline.Id.Value} aeropuerto={airportAirline.AirportId} aerolínea={airportAirline.AirlineId}."
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void GetAirportAirlineById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar relación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var airportAirline = _getByIdUseCase.ExecuteAsync(id).Result;
            if (airportAirline != null)
            {
                SpectreUi.ShowTable(
                    "Relación Aeropuerto ↔ Aerolínea",
                    ["Campo", "Valor"],
                    [
                        ["ID", airportAirline.Id.Value.ToString()],
                        ["AeropuertoId", airportAirline.AirportId.ToString()],
                        ["AerolíneaId", airportAirline.AirlineId.ToString()],
                        ["Terminal", airportAirline.Terminal.Value ?? ""],
                    ]
                );
            }
            else
            {
                Console.WriteLine("No encontrado");
            }
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void GetAllAirportAirlines()
    {
        try
        {
            var airportAirlines = _getAllUseCase.ExecuteAsync().Result.ToList();
            if (airportAirlines.Count == 0)
            {
                Console.WriteLine("No hay relaciones para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Aeropuerto ↔ Aerolínea", "Listado");
            SpectreUi.ShowTable(
                "Relaciones",
                ["ID", "Aeropuerto", "Aerolínea", "Terminal"],
                airportAirlines
                    .OrderBy(x => x.Id.Value)
                    .Select(x => (IReadOnlyList<string>)new[]
                    {
                        x.Id.Value.ToString(),
                        x.AirportId.ToString(),
                        x.AirlineId.ToString(),
                        x.Terminal.Value ?? ""
                    })
                    .ToList()
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void UpdateAirportAirline()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar relación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var airportId = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto", min: 1);
            var airlineId = SpectreUi.PromptIntRequiredCancelable("ID aerolínea", min: 1);
            var terminal = SpectreUi.PromptOptionalCancelable("Terminal", "opcional");
            var startDate = PromptDateOnlyRequired("Fecha inicio", "yyyy-MM-dd");
            var endDate = PromptDateOnlyOptional("Fecha fin", "yyyy-MM-dd (opcional)");
            var isActive = SpectreUi.PromptBool("¿Activa?", defaultValue: true);

            _updateUseCase.ExecuteAsync(
                    id,
                    airportId,
                    airlineId,
                    string.IsNullOrWhiteSpace(terminal) ? null : terminal,
                    startDate,
                    endDate,
                    isActive
                )
                .Wait();

            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void DeleteAirportAirline()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar relación", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            _deleteUseCase.ExecuteAsync(id).Wait();
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private static DateOnly PromptDateOnlyRequired(string label, string formatHint)
    {
        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(label, formatHint);
            if (DateOnly.TryParse(raw, out var date))
                return date;

            SpectreUi.MarkupLineOrPlain("[red]Fecha inválida.[/]", "Fecha inválida.");
        }
    }

    private static DateOnly? PromptDateOnlyOptional(string label, string formatHint)
    {
        while (true)
        {
            var raw = SpectreUi.PromptOptionalCancelable(label, formatHint);
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            if (DateOnly.TryParse(raw, out var date))
                return date;

            SpectreUi.MarkupLineOrPlain("[red]Fecha inválida.[/]", "Fecha inválida.");
        }
    }
}
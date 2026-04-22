using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airports.UI;

public class AirportConsoleUI : IModuleUI
{
    private readonly CreateAirportUseCase _createUseCase;
    private readonly GetAirportByIdUseCase _getByIdUseCase;
    private readonly GetAllAirportsUseCase _getAllUseCase;
    private readonly UpdateAirportUseCase _updateUseCase;
    private readonly DeleteAirportUseCase _deleteUseCase;

    public AirportConsoleUI(CreateAirportUseCase create, GetAirportByIdUseCase getById, GetAllAirportsUseCase getAll, UpdateAirportUseCase update, DeleteAirportUseCase delete)
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
            SpectreUi.ModuleHeader("Aeropuertos", "IATA / ICAO y ciudad");

            var items = new (string Label, Action Action)[]
            {
                ("Crear aeropuerto", CreateAirport),
                ("Listar todos", GetAllAirports),
                ("Consultar por ID", GetAirportById),
                ("Actualizar", UpdateAirport),
                ("Eliminar", DeleteAirport),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private void CreateAirport()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear aeropuerto", "Datos básicos");
            var name = SpectreUi.PromptRequired("Nombre");
            var iata = SpectreUi.PromptRequired("Código IATA", "3 letras (p.ej. BOG)");
            var icao = SpectreUi.PromptOptional("Código ICAO", "opcional (p.ej. SKBO)");
            var cityId = SpectreUi.PromptIntRequired("ID ciudad", min: 1);

            var airport = _createUseCase.ExecuteAsync(
                name,
                iata,
                string.IsNullOrWhiteSpace(icao) ? null : icao,
                cityId
            ).Result;

            SpectreUi.MarkupLineOrPlain(
                $"[green]Aeropuerto creado[/] id={airport.Id.Value} · [bold]{airport.Name.Value}[/] ({airport.IataCode.Value})",
                $"Aeropuerto creado id={airport.Id.Value} · {airport.Name.Value} ({airport.IataCode.Value})"
            );
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private void GetAirportById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar aeropuerto", null);
            var id = SpectreUi.PromptIntRequired("ID", min: 1);
            var airport = _getByIdUseCase.ExecuteAsync(id).Result;
            if (airport != null)
            {
                SpectreUi.ShowTable(
                    "Aeropuerto",
                    ["Campo", "Valor"],
                    [
                        ["ID", airport.Id.Value.ToString()],
                        ["Nombre", airport.Name.Value],
                        ["IATA", airport.IataCode.Value],
                        ["ICAO", airport.IcaoCode.Value ?? ""],
                    ]
                );
            }
            else
            {
                Console.WriteLine("No encontrado");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }

    private void GetAllAirports()
    {
        try
        {
            var airports = _getAllUseCase.ExecuteAsync().Result.ToList();
            if (airports.Count == 0)
            {
                Console.WriteLine("No hay aeropuertos para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Aeropuertos", "Listado");
            SpectreUi.ShowTable(
                "Aeropuertos",
                ["ID", "Nombre", "IATA", "ICAO"],
                airports
                    .OrderBy(a => a.Id.Value)
                    .Select(a => (IReadOnlyList<string>)new[]
                    {
                        a.Id.Value.ToString(),
                        a.Name.Value,
                        a.IataCode.Value,
                        a.IcaoCode.Value ?? ""
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

    private void UpdateAirport()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar aeropuerto", null);
            var id = SpectreUi.PromptIntRequired("ID", min: 1);
            var name = SpectreUi.PromptRequired("Nombre");
            var iata = SpectreUi.PromptRequired("Código IATA", "3 letras (p.ej. BOG)");
            var icao = SpectreUi.PromptOptional("Código ICAO", "opcional (p.ej. SKBO)");
            var cityId = SpectreUi.PromptIntRequired("ID ciudad", min: 1);

            _updateUseCase.ExecuteAsync(
                id,
                name,
                iata,
                string.IsNullOrWhiteSpace(icao) ? null : icao,
                cityId
            ).Wait();

            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }

    private void DeleteAirport()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar aeropuerto", null);
            var id = SpectreUi.PromptIntRequired("ID", min: 1);
            _deleteUseCase.ExecuteAsync(id).Wait();
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        SpectreUi.Pause();
    }
}
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Application.UseCases;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.UI;

public class AirlineConsoleUI : IModuleUI
{
    private readonly AppDbContext _ctx;
    private readonly CreateAirlineUseCase _createUseCase;
    private readonly GetAirlineByIdUseCase _getByIdUseCase;
    private readonly GetAllAirlinesUseCase _getAllUseCase;
    private readonly UpdateAirlineUseCase _updateUseCase;
    private readonly DeleteAirlineUseCase _deleteUseCase;

    public AirlineConsoleUI(
        AppDbContext ctx,
        CreateAirlineUseCase create,
        GetAirlineByIdUseCase getById,
        GetAllAirlinesUseCase getAll,
        UpdateAirlineUseCase update,
        DeleteAirlineUseCase delete
    )
    {
        _ctx = ctx;
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
            SpectreUi.ModuleHeader("Aerolíneas", "Gestión de aerolíneas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear aerolínea", CreateAirline),
                ("Listar todas", GetAllAirlines),
                ("Consultar por ID", GetAirlineById),
                ("Actualizar", UpdateAirline),
                ("Eliminar", DeleteAirline),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private void CreateAirline()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear aerolínea", "Datos básicos");
            SpectreUi.MarkupLineOrPlain("[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]", "Tip: escriba 0 / c / cancelar para salir sin guardar.");
            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            var iata = SpectreUi.PromptRequiredCancelable("Código IATA", "2-3 letras (p.ej. AV)");
            var countryName = SpectreUi.PromptRequiredCancelable("País de origen (nombre)", "p.ej. Colombia");

            var countryId = _ctx.Set<CountryEntity>()
                .AsNoTracking()
                .Where(c => c.IsActive && c.Name != null && c.Name.ToUpper() == countryName.Trim().ToUpper())
                .Select(c => c.Id)
                .FirstOrDefault();
            if (countryId < 1)
                throw new InvalidOperationException($"No existe un país activo con nombre '{countryName}'.");

            var airline = _createUseCase.ExecuteAsync(name, iata, countryId).Result;
            SpectreUi.MarkupLineOrPlain(
                $"[green]Aerolínea creada[/] id={airline.Id.Value} · [bold]{airline.Name.Value}[/] ({airline.IataCode.Value})",
                $"Aerolínea creada id={airline.Id.Value} · {airline.Name.Value} ({airline.IataCode.Value})"
            );
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

    private void GetAirlineById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar aerolínea", null);
            SpectreUi.MarkupLineOrPlain("[grey]Tip: escriba 0 / c / cancelar para volver.[/]", "Tip: escriba 0 / c / cancelar para volver.");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var airline = _getByIdUseCase.ExecuteAsync(id).Result;
            if (airline != null)
            {
                SpectreUi.ShowTable(
                    "Aerolínea",
                    ["Campo", "Valor"],
                    [
                        ["ID", airline.Id.Value.ToString()],
                        ["Nombre", airline.Name.Value],
                        ["IATA", airline.IataCode.Value],
                        ["Activa", airline.IsActive ? "Sí" : "No"],
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

    private void GetAllAirlines()
    {
        try
        {
            var airlines = _getAllUseCase.ExecuteAsync().Result.ToList();

            if (airlines.Count == 0)
            {
                Console.WriteLine("No hay aerolíneas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Aerolíneas", "Listado");
            SpectreUi.ShowTable(
                "Aerolíneas",
                ["ID", "Nombre", "IATA", "Activa"],
                airlines
                    .OrderBy(a => a.Id.Value)
                    .Select(a => (IReadOnlyList<string>)new[]
                    {
                        a.Id.Value.ToString(),
                        a.Name.Value,
                        a.IataCode.Value,
                        a.IsActive ? "Sí" : "No"
                    })
                    .ToList()
            );

            SpectreUi.Pause();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            SpectreUi.Pause();
        }
    }

    private void UpdateAirline()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar aerolínea", null);
            SpectreUi.MarkupLineOrPlain("[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]", "Tip: escriba 0 / c / cancelar para salir sin guardar.");
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var name = SpectreUi.PromptRequiredCancelable("Nombre");
            var iata = SpectreUi.PromptRequiredCancelable("Código IATA", "2-3 letras (p.ej. AV)");
            var countryName = SpectreUi.PromptRequiredCancelable("País de origen (nombre)", "p.ej. Colombia");
            var countryId = _ctx.Set<CountryEntity>()
                .AsNoTracking()
                .Where(c => c.IsActive && c.Name != null && c.Name.ToUpper() == countryName.Trim().ToUpper())
                .Select(c => c.Id)
                .FirstOrDefault();
            if (countryId < 1)
                throw new InvalidOperationException($"No existe un país activo con nombre '{countryName}'.");
            var isActive = SpectreUi.PromptBool("¿Activa?", defaultValue: true);

            _updateUseCase.ExecuteAsync(id, name, iata, countryId, isActive).Wait();
            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
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

    private void DeleteAirline()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar aerolínea", null);
            SpectreUi.MarkupLineOrPlain("[grey]Tip: escriba 0 / c / cancelar para volver.[/]", "Tip: escriba 0 / c / cancelar para volver.");
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
}
using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.UI;

public class RouteConsoleUI : IModuleUI
{
    private readonly CreateRouteUseCase _createUseCase;
    private readonly GetRouteByIdUseCase _getByIdUseCase;
    private readonly GetAllRoutesUseCase _getAllUseCase;
    private readonly UpdateRouteUseCase _updateUseCase;
    private readonly DeleteRouteUseCase _deleteUseCase;

    public RouteConsoleUI(CreateRouteUseCase createUseCase, GetRouteByIdUseCase getByIdUseCase, GetAllRoutesUseCase getAllUseCase, UpdateRouteUseCase updateUseCase, DeleteRouteUseCase deleteUseCase)
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
            SpectreUi.ModuleHeader("Rutas", "Gestión de rutas");

            var items = new (string Label, Action Action)[]
            {
                ("Crear ruta", () => CreateRoute().GetAwaiter().GetResult()),
                ("Listar todas", () => GetAllRoutes().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetRouteById().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateRoute().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteRoute().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateRoute()
    {
        try
        {
            SpectreUi.ModuleHeader("Crear ruta", "Origen → Destino");
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var origin = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto origen", min: 1);
            var dest = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto destino", min: 1);
            var dist = PromptNullableInt("Distancia (km)", "opcional");
            var dur = PromptNullableInt("Duración estimada (min)", "opcional");
            
            // Sugerir millas: Distancia * 10 para que sea una escala "real" pero alcanzable en el examen
            var suggestedMiles = dist.HasValue ? (dist.Value * 10m).ToString() : "5000";
            var milesRaw = SpectreUi.PromptOptionalCancelable("Millas por ticket", $"sugerido: {suggestedMiles}");
            var miles = decimal.Parse(string.IsNullOrWhiteSpace(milesRaw) ? suggestedMiles : milesRaw);
            
            await _createUseCase.ExecuteAsync(
                OriginAirportId.Create(origin),
                DestinationAirportId.Create(dest),
                DistanceKm.Create(dist),
                EstimatedDurationMin.Create(dur),
                RouteMiles.Create(miles)
            );
            SpectreUi.MarkupLineOrPlain("[green]Ruta creada.[/]", "Ruta creada.");
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

    private async Task GetRouteById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar ruta", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID ruta", min: 1);
            var route = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
            if (route != null)
            {
                SpectreUi.ShowTable(
                    "Ruta",
                    ["Campo", "Valor"],
                    [
                        ["ID", route.Id.Value.ToString()],
                        ["OrigenAirportId", route.OriginAirportId.Value.ToString()],
                        ["DestinoAirportId", route.DestinationAirportId.Value.ToString()],
                        ["Distancia (km)", route.DistanceKm.Value?.ToString() ?? ""],
                        ["Duración (min)", route.EstimatedDurationMin.Value?.ToString() ?? ""],
                        ["Millas", route.Miles.Value.ToString("0.##")],
                    ]
                );
            }
            else
            {
                Console.WriteLine("Ruta no encontrada");
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

    private async Task GetAllRoutes()
    {
        try
        {
            var routes = await _getAllUseCase.ExecuteAsync();
            var list = routes.ToList();
            if (list.Count == 0)
            {
                Console.WriteLine("No hay rutas para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Rutas", "Listado");
            SpectreUi.ShowTable(
                "Rutas",
                ["ID", "Origen", "Destino", "Km", "Min", "Millas"],
                list
                    .OrderBy(r => r.Id.Value)
                    .Select(r => (IReadOnlyList<string>)new[]
                    {
                        r.Id.Value.ToString(),
                        r.OriginAirportId.Value.ToString(),
                        r.DestinationAirportId.Value.ToString(),
                        r.DistanceKm.Value?.ToString() ?? "",
                        r.EstimatedDurationMin.Value?.ToString() ?? "",
                        r.Miles.Value.ToString("0.##")
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

    private async Task UpdateRoute()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar ruta", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID ruta", min: 1);
            var existing = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
            if (existing == null)
            {
                Console.WriteLine("Ruta no encontrada");
                SpectreUi.Pause();
                return;
            }

            var origin = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto origen", min: 1);
            var dest = SpectreUi.PromptIntRequiredCancelable("ID aeropuerto destino", min: 1);
            var dist = PromptNullableInt("Distancia (km)", "opcional");
            var dur = PromptNullableInt("Duración estimada (min)", "opcional");
            
            var suggestedMiles = dist.HasValue ? (dist.Value * 10m).ToString() : "5000";
            var milesRaw = SpectreUi.PromptOptionalCancelable("Millas por ticket", $"sugerido: {suggestedMiles}");
            var miles = decimal.Parse(string.IsNullOrWhiteSpace(milesRaw) ? suggestedMiles : milesRaw);
            
            await _updateUseCase.ExecuteAsync(
                RouteId.Create(id),
                OriginAirportId.Create(origin),
                DestinationAirportId.Create(dest),
                DistanceKm.Create(dist),
                EstimatedDurationMin.Create(dur),
                RouteMiles.Create(miles)
            );
            SpectreUi.MarkupLineOrPlain("[green]Ruta actualizada.[/]", "Ruta actualizada.");
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

    private async Task DeleteRoute()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar ruta", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID ruta", min: 1);
            await _deleteUseCase.ExecuteAsync(RouteId.Create(id));
            SpectreUi.MarkupLineOrPlain("[green]Ruta eliminada.[/]", "Ruta eliminada.");
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

    private static int? PromptNullableInt(string label, string? help = null)
    {
        while (true)
        {
            var raw = SpectreUi.PromptOptionalCancelable(label, help);
            if (string.IsNullOrWhiteSpace(raw))
                return null;
            if (int.TryParse(raw, out var value))
                return value;

            SpectreUi.MarkupLineOrPlain("[red]Número inválido.[/]", "Número inválido.");
        }
    }
}
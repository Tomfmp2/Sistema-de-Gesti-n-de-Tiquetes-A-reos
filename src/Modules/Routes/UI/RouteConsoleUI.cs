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
                ("Consultar por ID", () => GetRouteById().GetAwaiter().GetResult()),
                ("Listar todas", () => GetAllRoutes().GetAwaiter().GetResult()),
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
            Console.Write("ID aeropuerto origen: ");
            var origin = int.Parse(Console.ReadLine()!);
            Console.Write("ID aeropuerto destino: ");
            var dest = int.Parse(Console.ReadLine()!);
            Console.Write("Distancia km (opcional): ");
            var distStr = Console.ReadLine();
            int? dist = string.IsNullOrEmpty(distStr) ? null : int.Parse(distStr!);
            Console.Write("Duración estimada min (opcional): ");
            var durStr = Console.ReadLine();
            int? dur = string.IsNullOrEmpty(durStr) ? null : int.Parse(durStr!);
            await _createUseCase.ExecuteAsync(
                OriginAirportId.Create(origin),
                DestinationAirportId.Create(dest),
                DistanceKm.Create(dist),
                EstimatedDurationMin.Create(dur)
            );
            Console.WriteLine("Ruta creada");
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
            Console.Write("ID ruta: ");
            var id = int.Parse(Console.ReadLine()!);
            var route = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
            if (route != null)
            {
                Console.WriteLine($"ID: {route.Id.Value}, Origen: {route.OriginAirportId.Value}, Destino: {route.DestinationAirportId.Value}, Dist. km: {route.DistanceKm.Value}, Dur. min: {route.EstimatedDurationMin.Value}");
            }
            else
            {
                Console.WriteLine("Ruta no encontrada");
            }
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
            foreach (var r in routes)
            {
                Console.WriteLine($"ID: {r.Id.Value}, Origen: {r.OriginAirportId.Value}, Destino: {r.DestinationAirportId.Value}, Dist. km: {r.DistanceKm.Value}, Dur. min: {r.EstimatedDurationMin.Value}");
            }
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
            Console.Write("ID ruta: ");
            var id = int.Parse(Console.ReadLine()!);
            var existing = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
            if (existing == null)
            {
                Console.WriteLine("Ruta no encontrada");
                SpectreUi.Pause();
                return;
            }
            Console.Write("ID aeropuerto origen: ");
            var origin = int.Parse(Console.ReadLine()!);
            Console.Write("ID aeropuerto destino: ");
            var dest = int.Parse(Console.ReadLine()!);
            Console.Write("Distancia km (opcional): ");
            var distStr = Console.ReadLine();
            int? dist = string.IsNullOrEmpty(distStr) ? null : int.Parse(distStr!);
            Console.Write("Duración estimada min (opcional): ");
            var durStr = Console.ReadLine();
            int? dur = string.IsNullOrEmpty(durStr) ? null : int.Parse(durStr!);
            await _updateUseCase.ExecuteAsync(
                RouteId.Create(id),
                OriginAirportId.Create(origin),
                DestinationAirportId.Create(dest),
                DistanceKm.Create(dist),
                EstimatedDurationMin.Create(dur)
            );
            Console.WriteLine("Ruta actualizada");
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
            Console.Write("ID ruta: ");
            var id = int.Parse(Console.ReadLine()!);
            await _deleteUseCase.ExecuteAsync(RouteId.Create(id));
            Console.WriteLine("Ruta eliminada");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ExceptionFormatting.GetDiagnosticMessage(ex)}");
        }
        SpectreUi.Pause();
    }
}
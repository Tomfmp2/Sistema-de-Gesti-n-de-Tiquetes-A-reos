using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Routes.Domain.ValueObject;
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
        while (true)
        {
            Console.WriteLine("Gestión de rutas");
            Console.WriteLine("1. Crear ruta");
            Console.WriteLine("2. Consultar ruta por ID");
            Console.WriteLine("3. Listar todas las rutas");
            Console.WriteLine("4. Actualizar ruta");
            Console.WriteLine("5. Eliminar ruta");
            Console.WriteLine("0. Volver");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateRoute();
                    break;
                case "2":
                    await GetRouteById();
                    break;
                case "3":
                    await GetAllRoutes();
                    break;
                case "4":
                    await UpdateRoute();
                    break;
                case "5":
                    await DeleteRoute();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateRoute()
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

    private async Task GetRouteById()
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

    private async Task GetAllRoutes()
    {
        var routes = await _getAllUseCase.ExecuteAsync();
        foreach (var r in routes)
        {
            Console.WriteLine($"ID: {r.Id.Value}, Origen: {r.OriginAirportId.Value}, Destino: {r.DestinationAirportId.Value}, Dist. km: {r.DistanceKm.Value}, Dur. min: {r.EstimatedDurationMin.Value}");
        }
    }

    private async Task UpdateRoute()
    {
        Console.Write("ID ruta: ");
        var id = int.Parse(Console.ReadLine()!);
        var existing = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
        if (existing == null)
        {
            Console.WriteLine("Ruta no encontrada");
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

    private async Task DeleteRoute()
    {
        Console.Write("ID ruta: ");
        var id = int.Parse(Console.ReadLine()!);
        await _deleteUseCase.ExecuteAsync(RouteId.Create(id));
        Console.WriteLine("Ruta eliminada");
    }
}
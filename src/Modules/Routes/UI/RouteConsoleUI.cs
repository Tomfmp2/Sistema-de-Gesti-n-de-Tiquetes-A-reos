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
            Console.WriteLine("Route Management");
            Console.WriteLine("1. Create Route");
            Console.WriteLine("2. Get Route by ID");
            Console.WriteLine("3. Get All Routes");
            Console.WriteLine("4. Update Route");
            Console.WriteLine("5. Delete Route");
            Console.WriteLine("0. Back");
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
        Console.Write("Origin Airport ID: ");
        var origin = int.Parse(Console.ReadLine());
        Console.Write("Destination Airport ID: ");
        var dest = int.Parse(Console.ReadLine());
        Console.Write("Distance KM (optional): ");
        var distStr = Console.ReadLine();
        int? dist = string.IsNullOrEmpty(distStr) ? null : int.Parse(distStr);
        Console.Write("Estimated Duration Min (optional): ");
        var durStr = Console.ReadLine();
        int? dur = string.IsNullOrEmpty(durStr) ? null : int.Parse(durStr);
        await _createUseCase.ExecuteAsync(
            OriginAirportId.Create(origin),
            DestinationAirportId.Create(dest),
            DistanceKm.Create(dist),
            EstimatedDurationMin.Create(dur)
        );
        Console.WriteLine("Route created");
    }

    private async Task GetRouteById()
    {
        Console.Write("Route ID: ");
        var id = int.Parse(Console.ReadLine());
        var route = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
        if (route != null)
        {
            Console.WriteLine($"ID: {route.Id.Value}, Origin: {route.OriginAirportId.Value}, Dest: {route.DestinationAirportId.Value}, Dist: {route.DistanceKm.Value}, Dur: {route.EstimatedDurationMin.Value}");
        }
        else
        {
            Console.WriteLine("Route not found");
        }
    }

    private async Task GetAllRoutes()
    {
        var routes = await _getAllUseCase.ExecuteAsync();
        foreach (var r in routes)
        {
            Console.WriteLine($"ID: {r.Id.Value}, Origin: {r.OriginAirportId.Value}, Dest: {r.DestinationAirportId.Value}, Dist: {r.DistanceKm.Value}, Dur: {r.EstimatedDurationMin.Value}");
        }
    }

    private async Task UpdateRoute()
    {
        Console.Write("Route ID: ");
        var id = int.Parse(Console.ReadLine());
        var existing = await _getByIdUseCase.ExecuteAsync(RouteId.Create(id));
        if (existing == null)
        {
            Console.WriteLine("Route not found");
            return;
        }
        Console.Write("Origin Airport ID: ");
        var origin = int.Parse(Console.ReadLine());
        Console.Write("Destination Airport ID: ");
        var dest = int.Parse(Console.ReadLine());
        Console.Write("Distance KM (optional): ");
        var distStr = Console.ReadLine();
        int? dist = string.IsNullOrEmpty(distStr) ? null : int.Parse(distStr);
        Console.Write("Estimated Duration Min (optional): ");
        var durStr = Console.ReadLine();
        int? dur = string.IsNullOrEmpty(durStr) ? null : int.Parse(durStr);
        await _updateUseCase.ExecuteAsync(
            RouteId.Create(id),
            OriginAirportId.Create(origin),
            DestinationAirportId.Create(dest),
            DistanceKm.Create(dist),
            EstimatedDurationMin.Create(dur)
        );
        Console.WriteLine("Route updated");
    }

    private async Task DeleteRoute()
    {
        Console.Write("Route ID: ");
        var id = int.Parse(Console.ReadLine());
        await _deleteUseCase.ExecuteAsync(RouteId.Create(id));
        Console.WriteLine("Route deleted");
    }
}
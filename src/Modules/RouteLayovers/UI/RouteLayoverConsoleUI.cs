using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.RouteLayovers.UI;

public class RouteLayoverConsoleUI : IModuleUI
{
    private readonly CreateRouteLayoverUseCase _createUseCase;
    private readonly GetRouteLayoverByIdUseCase _getByIdUseCase;
    private readonly GetAllRouteLayoversUseCase _getAllUseCase;
    private readonly UpdateRouteLayoverUseCase _updateUseCase;
    private readonly DeleteRouteLayoverUseCase _deleteUseCase;

    public RouteLayoverConsoleUI(CreateRouteLayoverUseCase createUseCase, GetRouteLayoverByIdUseCase getByIdUseCase, GetAllRouteLayoversUseCase getAllUseCase, UpdateRouteLayoverUseCase updateUseCase, DeleteRouteLayoverUseCase deleteUseCase)
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
            Console.WriteLine("Route Layover Management");
            Console.WriteLine("1. Create Route Layover");
            Console.WriteLine("2. Get Route Layover by ID");
            Console.WriteLine("3. Get All Route Layovers");
            Console.WriteLine("4. Update Route Layover");
            Console.WriteLine("5. Delete Route Layover");
            Console.WriteLine("0. Back");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateRouteLayover();
                    break;
                case "2":
                    await GetRouteLayoverById();
                    break;
                case "3":
                    await GetAllRouteLayovers();
                    break;
                case "4":
                    await UpdateRouteLayover();
                    break;
                case "5":
                    await DeleteRouteLayover();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateRouteLayover()
    {
        Console.Write("Route ID: ");
        var routeId = int.Parse(Console.ReadLine());
        Console.Write("Layover Airport ID: ");
        var layoverAirportId = int.Parse(Console.ReadLine());
        Console.Write("Sequence Order: ");
        var sequenceOrder = int.Parse(Console.ReadLine());
        Console.Write("Layover Duration Min: ");
        var layoverDurationMin = int.Parse(Console.ReadLine());
        await _createUseCase.ExecuteAsync(
            RouteId.Create(routeId),
            LayoverAirportId.Create(layoverAirportId),
            SequenceOrder.Create(sequenceOrder),
            LayoverDurationMin.Create(layoverDurationMin)
        );
        Console.WriteLine("Route Layover created");
    }

    private async Task GetRouteLayoverById()
    {
        Console.Write("Route Layover ID: ");
        var id = int.Parse(Console.ReadLine());
        var routeLayover = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
        if (routeLayover != null)
        {
            Console.WriteLine($"ID: {routeLayover.Id.Value}, Route: {routeLayover.RouteId.Value}, Layover Airport: {routeLayover.LayoverAirportId.Value}, Sequence: {routeLayover.SequenceOrder.Value}, Duration: {routeLayover.LayoverDurationMin.Value}");
        }
        else
        {
            Console.WriteLine("Route Layover not found");
        }
    }

    private async Task GetAllRouteLayovers()
    {
        var routeLayovers = await _getAllUseCase.ExecuteAsync();
        foreach (var rl in routeLayovers)
        {
            Console.WriteLine($"ID: {rl.Id.Value}, Route: {rl.RouteId.Value}, Layover Airport: {rl.LayoverAirportId.Value}, Sequence: {rl.SequenceOrder.Value}, Duration: {rl.LayoverDurationMin.Value}");
        }
    }

    private async Task UpdateRouteLayover()
    {
        Console.Write("Route Layover ID: ");
        var id = int.Parse(Console.ReadLine());
        var existing = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
        if (existing == null)
        {
            Console.WriteLine("Route Layover not found");
            return;
        }
        Console.Write("Route ID: ");
        var routeId = int.Parse(Console.ReadLine());
        Console.Write("Layover Airport ID: ");
        var layoverAirportId = int.Parse(Console.ReadLine());
        Console.Write("Sequence Order: ");
        var sequenceOrder = int.Parse(Console.ReadLine());
        Console.Write("Layover Duration Min: ");
        var layoverDurationMin = int.Parse(Console.ReadLine());
        await _updateUseCase.ExecuteAsync(
            RouteLayoverId.Create(id),
            RouteId.Create(routeId),
            LayoverAirportId.Create(layoverAirportId),
            SequenceOrder.Create(sequenceOrder),
            LayoverDurationMin.Create(layoverDurationMin)
        );
        Console.WriteLine("Route Layover updated");
    }

    private async Task DeleteRouteLayover()
    {
        Console.Write("Route Layover ID: ");
        var id = int.Parse(Console.ReadLine());
        await _deleteUseCase.ExecuteAsync(RouteLayoverId.Create(id));
        Console.WriteLine("Route Layover deleted");
    }
}
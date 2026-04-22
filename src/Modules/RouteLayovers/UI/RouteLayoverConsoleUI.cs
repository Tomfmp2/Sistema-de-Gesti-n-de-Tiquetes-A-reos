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
            Console.WriteLine("Gestión de escalas en ruta");
            Console.WriteLine("1. Crear escala");
            Console.WriteLine("2. Consultar escala por ID");
            Console.WriteLine("3. Listar todas las escalas");
            Console.WriteLine("4. Actualizar escala");
            Console.WriteLine("5. Eliminar escala");
            Console.WriteLine("0. Volver");
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
        Console.Write("ID ruta: ");
        var routeId = int.Parse(Console.ReadLine()!);
        Console.Write("ID aeropuerto de escala: ");
        var layoverAirportId = int.Parse(Console.ReadLine()!);
        Console.Write("Orden en la secuencia: ");
        var sequenceOrder = int.Parse(Console.ReadLine()!);
        Console.Write("Duración de escala (min): ");
        var layoverDurationMin = int.Parse(Console.ReadLine()!);
        await _createUseCase.ExecuteAsync(
            RouteId.Create(routeId),
            LayoverAirportId.Create(layoverAirportId),
            SequenceOrder.Create(sequenceOrder),
            LayoverDurationMin.Create(layoverDurationMin)
        );
        Console.WriteLine("Escala creada");
    }

    private async Task GetRouteLayoverById()
    {
        Console.Write("ID escala: ");
        var id = int.Parse(Console.ReadLine()!);
        var routeLayover = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
        if (routeLayover != null)
        {
            Console.WriteLine($"ID: {routeLayover.Id.Value}, Ruta: {routeLayover.RouteId.Value}, Aeropuerto escala: {routeLayover.LayoverAirportId.Value}, Orden: {routeLayover.SequenceOrder.Value}, Duración min: {routeLayover.LayoverDurationMin.Value}");
        }
        else
        {
            Console.WriteLine("Escala no encontrada");
        }
    }

    private async Task GetAllRouteLayovers()
    {
        var routeLayovers = await _getAllUseCase.ExecuteAsync();
        foreach (var rl in routeLayovers)
        {
            Console.WriteLine($"ID: {rl.Id.Value}, Ruta: {rl.RouteId.Value}, Aeropuerto escala: {rl.LayoverAirportId.Value}, Orden: {rl.SequenceOrder.Value}, Duración min: {rl.LayoverDurationMin.Value}");
        }
    }

    private async Task UpdateRouteLayover()
    {
        Console.Write("ID escala: ");
        var id = int.Parse(Console.ReadLine()!);
        var existing = await _getByIdUseCase.ExecuteAsync(RouteLayoverId.Create(id));
        if (existing == null)
        {
            Console.WriteLine("Escala no encontrada");
            return;
        }
        Console.Write("ID ruta: ");
        var routeId = int.Parse(Console.ReadLine()!);
        Console.Write("ID aeropuerto de escala: ");
        var layoverAirportId = int.Parse(Console.ReadLine()!);
        Console.Write("Orden en la secuencia: ");
        var sequenceOrder = int.Parse(Console.ReadLine()!);
        Console.Write("Duración de escala (min): ");
        var layoverDurationMin = int.Parse(Console.ReadLine()!);
        await _updateUseCase.ExecuteAsync(
            RouteLayoverId.Create(id),
            RouteId.Create(routeId),
            LayoverAirportId.Create(layoverAirportId),
            SequenceOrder.Create(sequenceOrder),
            LayoverDurationMin.Create(layoverDurationMin)
        );
        Console.WriteLine("Escala actualizada");
    }

    private async Task DeleteRouteLayover()
    {
        Console.Write("ID escala: ");
        var id = int.Parse(Console.ReadLine()!);
        await _deleteUseCase.ExecuteAsync(RouteLayoverId.Create(id));
        Console.WriteLine("Escala eliminada");
    }
}
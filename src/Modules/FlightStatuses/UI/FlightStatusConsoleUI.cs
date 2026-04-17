using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightStatuses.UI;

public class FlightStatusConsoleUI : IModuleUI
{
    private readonly CreateFlightStatusUseCase _createUseCase;
    private readonly GetFlightStatusByIdUseCase _getByIdUseCase;
    private readonly GetAllFlightStatusesUseCase _getAllUseCase;
    private readonly UpdateFlightStatusUseCase _updateUseCase;
    private readonly DeleteFlightStatusUseCase _deleteUseCase;

    public FlightStatusConsoleUI(CreateFlightStatusUseCase createUseCase, GetFlightStatusByIdUseCase getByIdUseCase, GetAllFlightStatusesUseCase getAllUseCase, UpdateFlightStatusUseCase updateUseCase, DeleteFlightStatusUseCase deleteUseCase)
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
            Console.WriteLine("Flight Status Management");
            Console.WriteLine("1. Create Flight Status");
            Console.WriteLine("2. Get Flight Status by ID");
            Console.WriteLine("3. Get All Flight Statuses");
            Console.WriteLine("4. Update Flight Status");
            Console.WriteLine("5. Delete Flight Status");
            Console.WriteLine("0. Back");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateFlightStatus();
                    break;
                case "2":
                    await GetFlightStatusById();
                    break;
                case "3":
                    await GetAllFlightStatuses();
                    break;
                case "4":
                    await UpdateFlightStatus();
                    break;
                case "5":
                    await DeleteFlightStatus();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateFlightStatus()
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();
        await _createUseCase.ExecuteAsync(FlightStatusName.Create(name));
        Console.WriteLine("Flight Status created");
    }

    private async Task GetFlightStatusById()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());
        var flightStatus = await _getByIdUseCase.ExecuteAsync(FlightStatusId.Create(id));
        if (flightStatus != null)
        {
            Console.WriteLine($"ID: {flightStatus.Id.Value}, Name: {flightStatus.Name.Value}");
        }
        else
        {
            Console.WriteLine("Flight Status not found");
        }
    }

    private async Task GetAllFlightStatuses()
    {
        var statuses = await _getAllUseCase.ExecuteAsync();
        foreach (var s in statuses)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Name: {s.Name.Value}");
        }
    }

    private async Task UpdateFlightStatus()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());
        Console.Write("Name: ");
        var name = Console.ReadLine();
        await _updateUseCase.ExecuteAsync(FlightStatusId.Create(id), FlightStatusName.Create(name));
        Console.WriteLine("Flight Status updated");
    }

    private async Task DeleteFlightStatus()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());
        await _deleteUseCase.ExecuteAsync(FlightStatusId.Create(id));
        Console.WriteLine("Flight Status deleted");
    }
}
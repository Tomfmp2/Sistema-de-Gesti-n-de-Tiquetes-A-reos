using System;
using System.Threading.Tasks;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.UI;

public class SeatLocationTypeConsoleUI : IModuleUI
{
    private readonly CreateSeatLocationTypeUseCase _createUseCase;
    private readonly GetSeatLocationTypeByIdUseCase _getByIdUseCase;
    private readonly GetAllSeatLocationTypesUseCase _getAllUseCase;
    private readonly UpdateSeatLocationTypeUseCase _updateUseCase;
    private readonly DeleteSeatLocationTypeUseCase _deleteUseCase;

    public SeatLocationTypeConsoleUI(CreateSeatLocationTypeUseCase createUseCase, GetSeatLocationTypeByIdUseCase getByIdUseCase, GetAllSeatLocationTypesUseCase getAllUseCase, UpdateSeatLocationTypeUseCase updateUseCase, DeleteSeatLocationTypeUseCase deleteUseCase)
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
            Console.WriteLine("Seat Location Type Management");
            Console.WriteLine("1. Create Seat Location Type");
            Console.WriteLine("2. Get Seat Location Type by ID");
            Console.WriteLine("3. Get All Seat Location Types");
            Console.WriteLine("4. Update Seat Location Type");
            Console.WriteLine("5. Delete Seat Location Type");
            Console.WriteLine("0. Back");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateSeatLocationType();
                    break;
                case "2":
                    await GetSeatLocationTypeById();
                    break;
                case "3":
                    await GetAllSeatLocationTypes();
                    break;
                case "4":
                    await UpdateSeatLocationType();
                    break;
                case "5":
                    await DeleteSeatLocationType();
                    break;
                case "0":
                    return;
            }
        }
    }

    private async Task CreateSeatLocationType()
    {
        Console.Write("Name: ");
        var name = Console.ReadLine();
        await _createUseCase.ExecuteAsync(SeatLocationTypeName.Create(name));
        Console.WriteLine("Seat Location Type created");
    }

    private async Task GetSeatLocationTypeById()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());
        var seatLocationType = await _getByIdUseCase.ExecuteAsync(SeatLocationTypeId.Create(id));
        if (seatLocationType != null)
        {
            Console.WriteLine($"ID: {seatLocationType.Id.Value}, Name: {seatLocationType.Name.Value}");
        }
        else
        {
            Console.WriteLine("Seat Location Type not found");
        }
    }

    private async Task GetAllSeatLocationTypes()
    {
        var seatLocationTypes = await _getAllUseCase.ExecuteAsync();
        foreach (var s in seatLocationTypes)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Name: {s.Name.Value}");
        }
    }

    private async Task UpdateSeatLocationType()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());
        Console.Write("Name: ");
        var name = Console.ReadLine();
        await _updateUseCase.ExecuteAsync(SeatLocationTypeId.Create(id), SeatLocationTypeName.Create(name));
        Console.WriteLine("Seat Location Type updated");
    }

    private async Task DeleteSeatLocationType()
    {
        Console.Write("ID: ");
        var id = int.Parse(Console.ReadLine());
        await _deleteUseCase.ExecuteAsync(SeatLocationTypeId.Create(id));
        Console.WriteLine("Seat Location Type deleted");
    }
}
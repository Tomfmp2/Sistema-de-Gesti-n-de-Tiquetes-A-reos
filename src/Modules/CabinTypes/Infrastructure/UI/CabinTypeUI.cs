using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.UI;

public class CabinTypeUI : IModuleUI
{
    private readonly CreateCabinTypeUseCase _createUseCase;
    private readonly GetAllCabinTypesUseCase _getAllUseCase;
    private readonly GetCabinTypeByIdUseCase _getByIdUseCase;
    private readonly UpdateCabinTypeUseCase _updateUseCase;
    private readonly DeleteCabinTypeUseCase _deleteUseCase;

    public CabinTypeUI(AppDbContext context)
    {
        var repository = new CabinTypeRepository(context);
        _createUseCase = new CreateCabinTypeUseCase(repository);
        _getAllUseCase = new GetAllCabinTypesUseCase(repository);
        _getByIdUseCase = new GetCabinTypeByIdUseCase(repository);
        _updateUseCase = new UpdateCabinTypeUseCase(repository);
        _deleteUseCase = new DeleteCabinTypeUseCase(repository);
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Cabin Types Management ===");
            Console.WriteLine("1. Create Cabin Type");
            Console.WriteLine("2. View All Cabin Types");
            Console.WriteLine("3. View Cabin Type by ID");
            Console.WriteLine("4. Update Cabin Type");
            Console.WriteLine("5. Delete Cabin Type");
            Console.WriteLine("0. Back to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateCabinTypeAsync();
                    break;
                case "2":
                    await ViewAllCabinTypesAsync();
                    break;
                case "3":
                    await ViewCabinTypeByIdAsync();
                    break;
                case "4":
                    await UpdateCabinTypeAsync();
                    break;
                case "5":
                    await DeleteCabinTypeAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid option. Press any key to continue...");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private async Task CreateCabinTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Create Cabin Type ===");

        try
        {
            Console.Write("Name: ");
            var name = Console.ReadLine() ?? "";

            var cabinType = await _createUseCase.ExecuteAsync(name);
            Console.WriteLine($"Cabin Type created with ID: {cabinType.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewAllCabinTypesAsync()
    {
        Console.Clear();
        Console.WriteLine("=== All Cabin Types ===");

        try
        {
            var cabinTypes = await _getAllUseCase.ExecuteAsync();
            foreach (var ct in cabinTypes)
            {
                Console.WriteLine($"ID: {ct.Id.Value}, Name: {ct.Name.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task ViewCabinTypeByIdAsync()
    {
        Console.Clear();
        Console.WriteLine("=== View Cabin Type by ID ===");

        try
        {
            Console.Write("Cabin Type ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var cabinType = await _getByIdUseCase.ExecuteAsync(CabinTypeId.Create(id));
            if (cabinType != null)
            {
                Console.WriteLine($"ID: {cabinType.Id.Value}");
                Console.WriteLine($"Name: {cabinType.Name.Value}");
            }
            else
            {
                Console.WriteLine("Cabin Type not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task UpdateCabinTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Update Cabin Type ===");

        try
        {
            Console.Write("Cabin Type ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("New Name: ");
            var name = Console.ReadLine() ?? "";

            await _updateUseCase.ExecuteAsync(CabinTypeId.Create(id), name);
            Console.WriteLine("Cabin Type updated successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    private async Task DeleteCabinTypeAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Delete Cabin Type ===");

        try
        {
            Console.Write("Cabin Type ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(CabinTypeId.Create(id));
            Console.WriteLine("Cabin Type deleted successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
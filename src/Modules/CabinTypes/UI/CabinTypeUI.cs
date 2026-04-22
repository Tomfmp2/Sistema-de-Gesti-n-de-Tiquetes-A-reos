using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.repository;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.UI;

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
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Tipos de cabina", "Clases Económica, Business, etc.");

            var items = new (string Label, Action Action)[]
            {
                ("Crear tipo de cabina", () => CreateCabinTypeAsync().GetAwaiter().GetResult()),
                ("Listar todos", () => ViewAllCabinTypesAsync().GetAwaiter().GetResult()),
                ("Consultar por ID", () => ViewCabinTypeByIdAsync().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateCabinTypeAsync().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteCabinTypeAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateCabinTypeAsync()
    {
        SpectreUi.ModuleHeader("Crear tipo de cabina", null);
        try
        {
            Console.Write("Nombre: ");
            var name = Console.ReadLine() ?? "";

            var cabinType = await _createUseCase.ExecuteAsync(name);
            Console.WriteLine($"Tipo creado con ID: {cabinType.Id.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewAllCabinTypesAsync()
    {
        SpectreUi.ModuleHeader("Tipos de cabina", null);
        try
        {
            var cabinTypes = await _getAllUseCase.ExecuteAsync();
            foreach (var ct in cabinTypes)
            {
                Console.WriteLine($"ID: {ct.Id.Value}, Nombre: {ct.Name.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task ViewCabinTypeByIdAsync()
    {
        SpectreUi.ModuleHeader("Consultar tipo de cabina", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            var cabinType = await _getByIdUseCase.ExecuteAsync(CabinTypeId.Create(id));
            if (cabinType != null)
            {
                Console.WriteLine($"ID: {cabinType.Id.Value}");
                Console.WriteLine($"Nombre: {cabinType.Name.Value}");
            }
            else
            {
                Console.WriteLine("No encontrado.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task UpdateCabinTypeAsync()
    {
        SpectreUi.ModuleHeader("Actualizar tipo de cabina", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Nuevo nombre: ");
            var name = Console.ReadLine() ?? "";

            await _updateUseCase.ExecuteAsync(CabinTypeId.Create(id), name);
            Console.WriteLine("Actualizado correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }

    private async Task DeleteCabinTypeAsync()
    {
        SpectreUi.ModuleHeader("Eliminar tipo de cabina", null);
        try
        {
            Console.Write("ID: ");
            var id = int.Parse(Console.ReadLine() ?? "0");

            await _deleteUseCase.ExecuteAsync(CabinTypeId.Create(id));
            Console.WriteLine("Eliminado.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        SpectreUi.Pause();
    }
}

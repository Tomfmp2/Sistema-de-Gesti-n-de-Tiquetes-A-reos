using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.UI;

public class StaffConsoleUI : IModuleUI
{
    private readonly CreateStaffUseCase _createUseCase;
    private readonly GetStaffByIdUseCase _getByIdUseCase;
    private readonly GetAllStaffUseCase _getAllUseCase;
    private readonly UpdateStaffUseCase _updateUseCase;
    private readonly DeleteStaffUseCase _deleteUseCase;

    public StaffConsoleUI(
        CreateStaffUseCase createUseCase,
        GetStaffByIdUseCase getByIdUseCase,
        GetAllStaffUseCase getAllUseCase,
        UpdateStaffUseCase updateUseCase,
        DeleteStaffUseCase deleteUseCase)
    {
        _createUseCase = createUseCase;
        _getByIdUseCase = getByIdUseCase;
        _getAllUseCase = getAllUseCase;
        _updateUseCase = updateUseCase;
        _deleteUseCase = deleteUseCase;
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Personal", "Empleados de aerolínea o aeropuerto");

            var items = new (string Label, Action Action)[]
            {
                ("Registrar personal", () => CreateStaff().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetStaffById().GetAwaiter().GetResult()),
                ("Listar todo el personal", () => GetAllStaff().GetAwaiter().GetResult()),
                ("Actualizar", () => UpdateStaff().GetAwaiter().GetResult()),
                ("Eliminar", () => DeleteStaff().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private async Task CreateStaff()
    {
        try
        {
            Console.Write("Enter Person ID: ");
            var personId = int.Parse(Console.ReadLine()!);
            Console.Write("Enter Position ID: ");
            var positionId = int.Parse(Console.ReadLine()!);
            Console.Write("Enter Airline ID (optional): ");
            var airlineIdInput = Console.ReadLine();
            int? airlineId = string.IsNullOrWhiteSpace(airlineIdInput) ? null : int.Parse(airlineIdInput);
            Console.Write("Enter Airport ID (optional): ");
            var airportIdInput = Console.ReadLine();
            int? airportId = string.IsNullOrWhiteSpace(airportIdInput) ? null : int.Parse(airportIdInput);
            Console.Write("Enter Hire Date (YYYY-MM-DD): ");
            var hireDate = DateOnly.Parse(Console.ReadLine()!);
            Console.Write("Is Active (true/false): ");
            var isActive = bool.Parse(Console.ReadLine()!);

            var personIdVo = PersonId.Create(personId);
            var positionIdVo = PositionId.Create(positionId);
            var airlineIdVo = airlineId.HasValue ? AirlineId.Create(airlineId.Value) : null;
            var airportIdVo = airportId.HasValue ? AirportId.Create(airportId.Value) : null;
            var hireDateVo = HireDate.Create(hireDate);
            var isActiveVo = IsActive.Create(isActive);

            await _createUseCase.ExecuteAsync(personIdVo, positionIdVo, airlineIdVo, airportIdVo, hireDateVo, isActiveVo);
            Console.WriteLine("Staff created successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetStaffById()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffId = StaffId.Create(id);
                var staff = await _getByIdUseCase.ExecuteAsync(staffId);
                if (staff != null)
                {
                    Console.WriteLine($"ID: {staff.Id.Value}, Person ID: {staff.PersonId.Value}, Position ID: {staff.PositionId.Value}, Airline ID: {staff.AirlineId?.Value}, Airport ID: {staff.AirportId?.Value}, Hire Date: {staff.HireDate.Value}, Is Active: {staff.IsActive.Value}");
                }
                else
                {
                    Console.WriteLine("Staff not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task GetAllStaff()
    {
        var staffList = await _getAllUseCase.ExecuteAsync();
        foreach (var s in staffList)
        {
            Console.WriteLine($"ID: {s.Id.Value}, Person ID: {s.PersonId.Value}, Position ID: {s.PositionId.Value}, Airline ID: {s.AirlineId?.Value}, Airport ID: {s.AirportId?.Value}, Hire Date: {s.HireDate.Value}, Is Active: {s.IsActive.Value}");
        }
    }

    private async Task UpdateStaff()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffId = StaffId.Create(id);
                // For simplicity, update position and is active
                Console.Write("Enter new Position ID: ");
                var positionId = int.Parse(Console.ReadLine()!);
                Console.Write("Is Active (true/false): ");
                var isActive = bool.Parse(Console.ReadLine()!);

                var positionIdVo = PositionId.Create(positionId);
                var isActiveVo = IsActive.Create(isActive);

                await _updateUseCase.ExecuteAsync(staffId, positionId: positionIdVo, isActive: isActiveVo);
                Console.WriteLine("Staff updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private async Task DeleteStaff()
    {
        Console.Write("Enter ID: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            try
            {
                var staffId = StaffId.Create(id);
                await _deleteUseCase.ExecuteAsync(staffId);
                Console.WriteLine("Staff deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
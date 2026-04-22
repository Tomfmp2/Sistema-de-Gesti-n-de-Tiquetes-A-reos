using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.UseCases;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.UI;

public sealed class FlightAssignmentConsoleUI
{
    private readonly CreateFlightAssignmentUseCase _createUseCase;
    private readonly GetFlightAssignmentByIdUseCase _getByIdUseCase;
    private readonly GetAllFlightAssignmentsUseCase _getAllUseCase;
    private readonly UpdateFlightAssignmentUseCase _updateUseCase;
    private readonly DeleteFlightAssignmentUseCase _deleteUseCase;

    public FlightAssignmentConsoleUI(
        CreateFlightAssignmentUseCase createUseCase,
        GetFlightAssignmentByIdUseCase getByIdUseCase,
        GetAllFlightAssignmentsUseCase getAllUseCase,
        UpdateFlightAssignmentUseCase updateUseCase,
        DeleteFlightAssignmentUseCase deleteUseCase)
    {
        _createUseCase = createUseCase ?? throw new ArgumentNullException(nameof(createUseCase));
        _getByIdUseCase = getByIdUseCase ?? throw new ArgumentNullException(nameof(getByIdUseCase));
        _getAllUseCase = getAllUseCase ?? throw new ArgumentNullException(nameof(getAllUseCase));
        _updateUseCase = updateUseCase ?? throw new ArgumentNullException(nameof(updateUseCase));
        _deleteUseCase = deleteUseCase ?? throw new ArgumentNullException(nameof(deleteUseCase));
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Asignaciones de tripulación ===");
            Console.WriteLine("1. Crear asignación");
            Console.WriteLine("2. Consultar asignación por ID");
            Console.WriteLine("3. Listar todas las asignaciones");
            Console.WriteLine("4. Actualizar asignación");
            Console.WriteLine("5. Eliminar asignación");
            Console.WriteLine("6. Salir");
            Console.Write("Elija una opción: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await CreateFlightAssignmentAsync();
                    break;
                case "2":
                    await GetFlightAssignmentByIdAsync();
                    break;
                case "3":
                    await GetAllFlightAssignmentsAsync();
                    break;
                case "4":
                    await UpdateFlightAssignmentAsync();
                    break;
                case "5":
                    await DeleteFlightAssignmentAsync();
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            }
        }
    }

    private async Task CreateFlightAssignmentAsync()
    {
        Console.Write("ID vuelo: ");
        if (!int.TryParse(Console.ReadLine(), out int flightId) || flightId <= 0)
        {
            Console.WriteLine("ID de vuelo no válido.");
            return;
        }

        Console.Write("ID personal: ");
        if (!int.TryParse(Console.ReadLine(), out int staffId) || staffId <= 0)
        {
            Console.WriteLine("ID de personal no válido.");
            return;
        }

        Console.Write("ID rol de vuelo: ");
        if (!int.TryParse(Console.ReadLine(), out int flightRoleId) || flightRoleId <= 0)
        {
            Console.WriteLine("ID de rol de vuelo no válido.");
            return;
        }

        try
        {
            var flightAssignment = await _createUseCase.ExecuteAsync(flightId, staffId, flightRoleId);
            Console.WriteLine($"Asignación creada correctamente (ID: {flightAssignment.Id.Value})");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetFlightAssignmentByIdAsync()
    {
        Console.Write("ID asignación: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        try
        {
            var flightAssignment = await _getByIdUseCase.ExecuteAsync(id);
            
            if (flightAssignment == null)
            {
                Console.WriteLine("Asignación no encontrada.");
                return;
            }

            Console.WriteLine($"\nID: {flightAssignment.Id.Value}");
            Console.WriteLine($"ID vuelo: {flightAssignment.FlightId.Value}");
            Console.WriteLine($"ID personal: {flightAssignment.StaffId.Value}");
            Console.WriteLine($"ID rol de vuelo: {flightAssignment.FlightRoleId.Value}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task GetAllFlightAssignmentsAsync()
    {
        try
        {
            var flightAssignments = await _getAllUseCase.ExecuteAsync();
            
            var list = flightAssignments.ToList();
            if (!list.Any())
            {
                Console.WriteLine("No hay asignaciones registradas.");
                return;
            }

            Console.WriteLine("\n=== Asignaciones ===");
            foreach (var fa in list)
            {
                Console.WriteLine($"ID: {fa.Id.Value} | Vuelo: {fa.FlightId.Value} | Personal: {fa.StaffId.Value} | Rol: {fa.FlightRoleId.Value}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task UpdateFlightAssignmentAsync()
    {
        Console.Write("ID asignación: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        Console.Write("Nuevo ID vuelo (Enter para omitir): ");
        int? newFlightId = null;
        string? flightInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(flightInput) && int.TryParse(flightInput, out int fid) && fid > 0)
            newFlightId = fid;

        Console.Write("Nuevo ID personal (Enter para omitir): ");
        int? newStaffId = null;
        string? staffInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(staffInput) && int.TryParse(staffInput, out int sid) && sid > 0)
            newStaffId = sid;

        Console.Write("Nuevo ID rol de vuelo (Enter para omitir): ");
        int? newFlightRoleId = null;
        string? roleInput = Console.ReadLine();
        if (!string.IsNullOrEmpty(roleInput) && int.TryParse(roleInput, out int rid) && rid > 0)
            newFlightRoleId = rid;

        try
        {
            var flightAssignment = await _updateUseCase.ExecuteAsync(id, newFlightId, newStaffId, newFlightRoleId);
            
            if (flightAssignment == null)
            {
                Console.WriteLine("Asignación no encontrada.");
                return;
            }

            Console.WriteLine("Asignación actualizada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private async Task DeleteFlightAssignmentAsync()
    {
        Console.Write("ID asignación a eliminar: ");
        if (!int.TryParse(Console.ReadLine(), out int id) || id <= 0)
        {
            Console.WriteLine("ID no válido.");
            return;
        }

        try
        {
            bool deleted = await _deleteUseCase.ExecuteAsync(id);
            
            if (!deleted)
            {
                Console.WriteLine("Asignación no encontrada.");
                return;
            }

            Console.WriteLine("Asignación eliminada correctamente.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}

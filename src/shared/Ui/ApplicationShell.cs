using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

/// <summary>
/// Menú principal de la aplicación (español). Los módulos se resuelven con <see cref="ModuleUiFactory"/>.
/// </summary>
public static class ApplicationShell
{
    private static string NormalizeRoleKey(string input) =>
        string.Concat((input ?? string.Empty)
            .Trim()
            .ToLowerInvariant()
            .Where(char.IsLetterOrDigit));

    private static void RunAirportAdministrationMenu(AppDbContext context)
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader(
                "Administración aeroportuaria",
                "Gestión operativa del aeropuerto, ventas y atención al cliente"
            );

            var items = new List<(string Label, Action Action)>
            {
                // Administración del sistema
                ("Usuarios", () => ModuleUiFactory.CreateUserUi(context).RunAsync().GetAwaiter().GetResult()),

                // Catálogos y operación aeroportuaria
                ("Aerolíneas", () => ModuleUiFactory.CreateAirlineUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Aeropuertos", () => ModuleUiFactory.CreateAirportUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Aeropuerto ↔ Aerolínea", () => ModuleUiFactory.CreateAirportAirlineUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Rutas", () => ModuleUiFactory.CreateRouteUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Escalas de ruta", () => ModuleUiFactory.CreateRouteLayoverUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Temporadas", () => ModuleUiFactory.CreateSeasonUi(context).RunAsync().GetAwaiter().GetResult()),

                // Flota / cabina / asientos
                ("Fabricantes de avión", () => ModuleUiFactory.CreateAircraftManufacturerUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Modelos de avión", () => ModuleUiFactory.CreateAircraftModelUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Aviones", () => ModuleUiFactory.CreateAircraftUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Tipos de cabina", () => ModuleUiFactory.CreateCabinTypeUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Configuración de cabina", () => ModuleUiFactory.CreateCabinConfigurationUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Tipos de ubicación de asiento", () => ModuleUiFactory.CreateSeatLocationTypeUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Asientos por vuelo", () => ModuleUiFactory.CreateFlightSeatUi(context).RunAsync().GetAwaiter().GetResult()),

                // Vuelos / tripulación
                ("Vuelos", () => ModuleUiFactory.CreateFlightUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Estados de vuelo", () => ModuleUiFactory.CreateFlightStatusUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Roles de vuelo", () => ModuleUiFactory.CreateFlightRoleUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Asignaciones de tripulación", () => ModuleUiFactory.CreateFlightAssignmentUi(context).RunAsync().GetAwaiter().GetResult()),

                // Personal
                ("Personal", () => ModuleUiFactory.CreateStaffUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Cargos", () => ModuleUiFactory.CreateStaffPositionUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Estados de disponibilidad", () => ModuleUiFactory.CreateAvailabilityStatusUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Disponibilidad del personal", () => ModuleUiFactory.CreateStaffAvailabilityUi(context).RunAsync().GetAwaiter().GetResult()),

                // Equipaje
                ("Tipos de equipaje", () => ModuleUiFactory.CreateBaggageTypeUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Equipaje", () => ModuleUiFactory.CreateBaggageUi(context).RunAsync().GetAwaiter().GetResult()),

                // Venta / post-venta
                ("Tarifas", () => ModuleUiFactory.CreateFareUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Reservaciones (para clientes)", () => ModuleUiFactory.CreateAgentReservationsUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Pagos", () => ModuleUiFactory.CreatePaymentUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Tickets", () => ModuleUiFactory.CreateTicketUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Check-ins", () => ModuleUiFactory.CreateCheckinUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Facturas", () => ModuleUiFactory.CreateInvoiceUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Programa de Fidelización (Millas)", () => ModuleUiFactory.CreateMilesTransactionUi(context).RunAsync().GetAwaiter().GetResult()),

                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    private static void RunOperationsMenu(AppDbContext context)
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader(
                "Operación aeroportuaria",
                "Gestión operativa de vuelos, control, recaudo y documentos"
            );

            var items = new List<(string Label, Action Action)>
            {
                ("Vuelos", () => ModuleUiFactory.CreateFlightUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Tarifas", () => ModuleUiFactory.CreateFareUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Reservaciones (consulta/gestión)", () => ModuleUiFactory.CreateAgentReservationsUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Pagos", () => ModuleUiFactory.CreatePaymentUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Tickets", () => ModuleUiFactory.CreateTicketUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Check-ins", () => ModuleUiFactory.CreateCheckinUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Facturas", () => ModuleUiFactory.CreateInvoiceUi(context).RunAsync().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(items);
        }
    }

    /// <summary>
    /// Catálogos y operación (flota, red, tripulación, etc.) para rol administrador o ROOT.
    /// </summary>
    private static void AppendAdministrationModules(
        List<(string Label, Action Action)> items,
        AppDbContext context
    )
    {
        void Add(string label, Func<Task> runAsync) =>
            items.Add((label, () => runAsync().GetAwaiter().GetResult()));

        Add("Aerolíneas", () => ModuleUiFactory.CreateAirlineUi(context).RunAsync());
        Add("Aeropuertos", () => ModuleUiFactory.CreateAirportUi(context).RunAsync());
        Add("Aeropuerto ↔ Aerolínea", () => ModuleUiFactory.CreateAirportAirlineUi(context).RunAsync());
        Add("Rutas", () => ModuleUiFactory.CreateRouteUi(context).RunAsync());
        Add("Escalas de ruta", () => ModuleUiFactory.CreateRouteLayoverUi(context).RunAsync());
        Add("Temporadas", () => ModuleUiFactory.CreateSeasonUi(context).RunAsync());
        Add("Fabricantes de avión", () => ModuleUiFactory.CreateAircraftManufacturerUi(context).RunAsync());
        Add("Modelos de avión", () => ModuleUiFactory.CreateAircraftModelUi(context).RunAsync());
        Add("Aviones", () => ModuleUiFactory.CreateAircraftUi(context).RunAsync());
        Add("Tipos de cabina", () => ModuleUiFactory.CreateCabinTypeUi(context).RunAsync());
        Add("Configuración de cabina", () => ModuleUiFactory.CreateCabinConfigurationUi(context).RunAsync());
        Add("Tipos de ubicación de asiento", () => ModuleUiFactory.CreateSeatLocationTypeUi(context).RunAsync());
        Add("Estados de vuelo", () => ModuleUiFactory.CreateFlightStatusUi(context).RunAsync());
        Add("Roles de vuelo", () => ModuleUiFactory.CreateFlightRoleUi(context).RunAsync());
        Add("Asientos por vuelo", () => ModuleUiFactory.CreateFlightSeatUi(context).RunAsync());
        Add(
            "Asignaciones de tripulación",
            () => ModuleUiFactory.CreateFlightAssignmentUi(context).RunAsync()
        );
        Add("Personal", () => ModuleUiFactory.CreateStaffUi(context).RunAsync());
        Add("Cargos", () => ModuleUiFactory.CreateStaffPositionUi(context).RunAsync());
        Add("Estados de disponibilidad", () => ModuleUiFactory.CreateAvailabilityStatusUi(context).RunAsync());
        Add("Disponibilidad del personal", () => ModuleUiFactory.CreateStaffAvailabilityUi(context).RunAsync());
        Add("Tipos de equipaje", () => ModuleUiFactory.CreateBaggageTypeUi(context).RunAsync());
        Add("Equipaje", () => ModuleUiFactory.CreateBaggageUi(context).RunAsync());
        Add("Programa de Fidelización (Millas)", () => ModuleUiFactory.CreateMilesTransactionUi(context).RunAsync());
    }

    public static async Task<AppExitReason?> RunAsync(
        AppDbContext context,
        AuthContext auth,
        CancellationToken cancellationToken = default
    )
    {
        var shouldLogout = false;
        while (!cancellationToken.IsCancellationRequested)
        {
            SpectreUi.Clear();
            SpectreUi.ShowAppBanner();
            SpectreUi.MarkupLineOrPlain(
                $"[grey]Usuario:[/] [bold]{auth.Username}[/]  ·  [grey]Rol:[/] [bold]{auth.RoleName}[/]  [dim](soporte: user {auth.UserId} · sesión {auth.SessionId})[/]",
                $"Usuario: {auth.Username} · Rol: {auth.RoleName} (soporte: user {auth.UserId}, sesión {auth.SessionId})"
            );

            var items = new List<(string Label, Action Action)>();

            // - ROOT: mismos módulos de administración + "Mis reservaciones"
            // - admin: todos los módulos de administración/operación disponibles en consola
            // - usuario normal: solo sus reservaciones
            var isRoot = string.Equals(auth.Username, "ROOT", StringComparison.OrdinalIgnoreCase);
            var roleKey = NormalizeRoleKey(auth.RoleName);
            var isAdmin =
                auth.RoleId == 1
                || roleKey == "admin"
                || roleKey == "administrador"
                || roleKey == "administradora"
                || roleKey.Contains("administrador", StringComparison.Ordinal);
            var isOperations = roleKey == "operaciones" || roleKey.Contains("operaciones", StringComparison.Ordinal);

            if (isRoot)
            {
                items.Add(("Usuarios", () => ModuleUiFactory.CreateUserUi(context).RunAsync().GetAwaiter().GetResult()));
                AppendAdministrationModules(items, context);
                items.Add((
                    "Mis reservaciones",
                    () => ModuleUiFactory.CreateMyReservationsUi(context, auth).RunAsync().GetAwaiter().GetResult()
                ));
            }
            else if (isAdmin)
            {
                items.Add(("Administración aeroportuaria", () => RunAirportAdministrationMenu(context)));
            }
            else if (isOperations)
            {
                items.Add(("Operación aeroportuaria", () => RunOperationsMenu(context)));
            }
            else
            {
                items.Add((
                    "Reservaciones",
                    () => ModuleUiFactory.CreateClientReservationsUi(context, auth).RunAsync().GetAwaiter().GetResult()
                ));
                items.Add((
                    "Mis Millas (Fidelización)",
                    () => ModuleUiFactory.CreateMilesTransactionUi(context, auth).RunAsync().GetAwaiter().GetResult()
                ));
            }

            items.Add((
                "Cerrar sesión",
                () =>
                {
                    LoginShell.LogoutAsync(context, auth).GetAwaiter().GetResult();
                    SpectreUi.MarkupLineOrPlain("[grey]Sesión cerrada.[/]", "Sesión cerrada.");
                    shouldLogout = true;
                }
            ));

            items.Add((
                "Salir",
                () =>
                {
                    SpectreUi.MarkupLineOrPlain("[grey]Hasta luego.[/]", "Hasta luego.");
                    Environment.Exit(0);
                }
            ));

            MenuLogic.RunMenu(
                items,
                "[bold]¿Qué deseas hacer?[/]\n[grey]Flechas arriba/abajo y Enter para confirmar. "
                    + "«Cerrar sesión» vuelve al login. «Salir» cierra el programa.[/]"
            );

            if (shouldLogout)
            {
                return AppExitReason.Logout;
            }
        }

        return null;
    }
}

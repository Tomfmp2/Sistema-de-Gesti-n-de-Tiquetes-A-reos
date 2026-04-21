using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using Spectre.Console;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

/// <summary>
/// Menú principal de la aplicación (español). Los módulos se resuelven con <see cref="ModuleUiFactory"/>.
/// </summary>
public static class ApplicationShell
{
    public static async Task RunAsync(
        AppDbContext context,
        CancellationToken cancellationToken = default
    )
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            SpectreUi.Clear();
            SpectreUi.ShowAppBanner();

            var items = new (string Label, Action Action)[]
            {
                (
                    "Aerolíneas",
                    () =>
                        ModuleUiFactory.CreateAirlineUi(context).RunAsync().GetAwaiter().GetResult()
                ),
                (
                    "Aeropuertos",
                    () =>
                        ModuleUiFactory.CreateAirportUi(context).RunAsync().GetAwaiter().GetResult()
                ),
                (
                    "Aeropuerto ↔ Aerolínea",
                    () =>
                        ModuleUiFactory
                            .CreateAirportAirlineUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Fabricantes de aeronaves",
                    () =>
                        ModuleUiFactory
                            .CreateAircraftManufacturerUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Modelos de aeronave",
                    () =>
                        ModuleUiFactory
                            .CreateAircraftModelUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Aeronaves",
                    () =>
                        ModuleUiFactory
                            .CreateAircraftUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Tipos de cabina",
                    () =>
                        ModuleUiFactory
                            .CreateCabinTypeUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Configuración de cabina",
                    () =>
                        ModuleUiFactory
                            .CreateCabinConfigurationUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Estados de vuelo",
                    () =>
                        ModuleUiFactory
                            .CreateFlightStatusUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Roles de vuelo",
                    () =>
                        ModuleUiFactory
                            .CreateFlightRoleUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Personal",
                    () => ModuleUiFactory.CreateStaffUi(context).RunAsync().GetAwaiter().GetResult()
                ),
                (
                    "Cargos de personal",
                    () =>
                        ModuleUiFactory
                            .CreateStaffPositionUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Estados de disponibilidad",
                    () =>
                        ModuleUiFactory
                            .CreateAvailabilityStatusUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Disponibilidad de personal",
                    () =>
                        ModuleUiFactory
                            .CreateStaffAvailabilityUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Tipos de equipaje",
                    () =>
                        ModuleUiFactory
                            .CreateBaggageTypeUi(context)
                            .RunAsync()
                            .GetAwaiter()
                            .GetResult()
                ),
                (
                    "Equipaje registrado",
                    () =>
                        ModuleUiFactory.CreateBaggageUi(context).RunAsync().GetAwaiter().GetResult()
                ),
                (
                    "Salir",
                    () =>
                    {
                        AnsiConsole.MarkupLine("[grey]Hasta luego.[/]");
                        Environment.Exit(0);
                    }
                ),
            };

            MenuLogic.RunMenu(
                items,
                "[bold]Módulos del sistema[/]\n[grey]↑/↓ para moverse · Enter para elegir[/]"
            );
        }
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.Application.Services;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.MilesTransactions.UI;

public sealed class MilesTransactionConsoleUI : IModuleUI
{
    private readonly IMilesTransactionService _service;
    private readonly AuthContext? _auth;
    private readonly Func<decimal, decimal, Task>? _reserveWithDiscount;

    // Tabla de tramos de descuento por millas
    private static readonly (decimal Miles, decimal DiscountPct, string Label)[] DiscountTiers =
    [
        (25_000m, 10m, "10% de descuento"),
        (80_000m, 25m, "25% de descuento"),
        (150_000m, 50m, "50% de descuento"),
        (500_000m, 100m, "Vuelo GRATIS (100%)")
    ];

    public MilesTransactionConsoleUI(
        IMilesTransactionService service,
        AuthContext? auth = null,
        Func<decimal, decimal, Task>? reserveWithDiscount = null)
    {
        _service = service;
        _auth = auth;
        _reserveWithDiscount = reserveWithDiscount;
    }

    public async Task RunAsync()
    {
        bool exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Programa de Fidelización (Millas)", "Gestión y analítica del programa de millas");

            var items = new List<(string Label, Action Action)>();

            if (_auth != null && _auth.ClientId.HasValue)
            {
                items.Add(("Ver mi saldo de millas", () => GetMyBalance()));
                items.Add(("Redimir mis millas (descuento en vuelo)", () => RedeemMyMiles().GetAwaiter().GetResult()));
            }
            else
            {
                items.Add(("Consultar saldo de cliente", GetBalance));
                items.Add(("Simular acumulación (post-vuelo)", SimulateAccumulation));
                items.Add(("Simular redención (al reservar)", SimulateRedemption));
                items.Add(("[blue]Reporte: Clientes con más millas acumuladas[/]", () => GetTopAccumulators().Wait()));
                items.Add(("[blue]Reporte: Clientes que más redimen[/]", () => GetTopRedeemers().Wait()));
                items.Add(("[blue]Reporte: Aerolíneas con mayor volumen de fidelización[/]", () => GetTopAirlines().Wait()));
                items.Add(("[blue]Reporte: Rutas con mayor acumulación de millas[/]", () => GetTopRoutes().Wait()));
                items.Add(("[blue]Reporte: Ranking de viajeros frecuentes[/]", () => GetFrequentFlyers().Wait()));
            }

            items.Add(("Volver", () => exit = true));

            MenuLogic.RunMenu(items.ToArray());
        }
    }

    // ═══════════════════════════════════════════════════════════
    //  FLUJO PRINCIPAL: REDENCIÓN DE MILLAS (CLIENTE)
    // ═══════════════════════════════════════════════════════════

    private async Task RedeemMyMiles()
    {
        try
        {
            SpectreUi.ModuleHeader("Redimir Millas", "Canjea tus millas por descuentos en vuelos");

            var clientId = _auth!.ClientId!.Value;
            var balance = await _service.GetClientBalanceAsync(clientId);

            // ── 1. Mostrar saldo actual ──────────────────────────
            SpectreUi.MarkupLineOrPlain(
                $"\n[bold]Tu saldo actual:[/] [green bold]{balance:N0} millas[/]\n",
                $"\nTu saldo actual: {balance:N0} millas\n"
            );

            // ── 2. Mostrar tabla de descuentos ───────────────────
            var discountRows = new List<IReadOnlyList<string>>();
            decimal maxApplicablePct = 0m;

            foreach (var (miles, pct, label) in DiscountTiers)
            {
                var available = balance >= miles;
                var status = available ? "Disponible" : "Insuficiente";
                discountRows.Add(new[]
                {
                    $"{miles:N0}",
                    label,
                    status
                });
                if (available) maxApplicablePct = pct;
            }

            SpectreUi.ShowTable(
                "Tabla de Descuentos por Millas",
                ["Millas requeridas", "Descuento", "Estado"],
                discountRows
            );

            if (maxApplicablePct > 0m)
            {
                SpectreUi.MarkupLineOrPlain(
                    $"[bold green]→ Tu descuento máximo disponible: {maxApplicablePct}%[/]\n",
                    $"→ Tu descuento máximo disponible: {maxApplicablePct}%\n"
                );
            }
            else
            {
                SpectreUi.MarkupLineOrPlain(
                    "[yellow]No tienes suficientes millas para ningún descuento. Necesitas al menos 25,000 millas.[/]",
                    "No tienes suficientes millas para ningún descuento. Necesitas al menos 25,000 millas."
                );
                SpectreUi.Pause();
                return;
            }

            // ── 3. Seleccionar descuento ─────────────────────────
            var availableTiers = DiscountTiers.Where(t => balance >= t.Miles).ToList();

            var menuItems = new List<(string Label, Action Action)>();
            decimal selectedPct = 0m;
            decimal selectedMiles = 0m;
            bool confirmed = false;

            foreach (var tier in availableTiers)
            {
                var capturedTier = tier;
                menuItems.Add(($"{capturedTier.Label} ({capturedTier.Miles:N0} millas)", () =>
                {
                    selectedPct = capturedTier.DiscountPct;
                    selectedMiles = capturedTier.Miles;
                    confirmed = true;
                }));
            }

            menuItems.Add(("Cancelar (volver sin redimir)", () => { confirmed = false; }));

            SpectreUi.MarkupLineOrPlain(
                "[bold]¿A cuál descuento deseas aplicar?[/]",
                "¿A cuál descuento deseas aplicar?"
            );
            MenuLogic.RunMenu(menuItems.ToArray());

            if (!confirmed)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[grey]Redención cancelada.[/]",
                    "Redención cancelada."
                );
                SpectreUi.Pause();
                return;
            }

            // ── 4. Confirmar la redención ────────────────────────
            SpectreUi.MarkupLineOrPlain(
                $"\n[bold]Has seleccionado:[/] [green]{selectedPct}% de descuento[/] por [yellow]{selectedMiles:N0} millas[/].",
                $"\nHas seleccionado: {selectedPct}% de descuento por {selectedMiles:N0} millas."
            );
            SpectreUi.MarkupLineOrPlain(
                "[grey]A continuación se iniciará el proceso de reservación con tu descuento aplicado.[/]\n",
                "A continuación se iniciará el proceso de reservación con tu descuento aplicado.\n"
            );

            // ── 5. Iniciar flujo de reserva con descuento ────────
            if (_reserveWithDiscount == null)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[red]Error: No se pudo conectar con el módulo de reservaciones.[/]",
                    "Error: No se pudo conectar con el módulo de reservaciones."
                );
                SpectreUi.Pause();
                return;
            }

            await _reserveWithDiscount(selectedPct, selectedMiles);

            // Mostrar saldo actualizado
            var newBalance = await _service.GetClientBalanceAsync(clientId);
            SpectreUi.MarkupLineOrPlain(
                $"\n[bold]Saldo de millas actualizado:[/] [green]{newBalance:N0} millas[/]",
                $"\nSaldo de millas actualizado: {newBalance:N0} millas"
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.MarkupLineOrPlain(
                $"[red]Error:[/] {ex.InnerException?.Message ?? ex.Message}",
                $"Error: {ex.InnerException?.Message ?? ex.Message}"
            );
        }
        SpectreUi.Pause();
    }

    // ═══════════════════════════════════════════════════════════
    //  ACCIONES DE SALDO (CLIENTE)
    // ═══════════════════════════════════════════════════════════

    private void GetMyBalance()
    {
        try
        {
            SpectreUi.ModuleHeader("Mi Saldo de Millas", null);
            var balance = _service.GetClientBalanceAsync(_auth!.ClientId!.Value).Result;
            
            SpectreUi.MarkupLineOrPlain($"[green]Tu saldo actual es: {balance:N0} millas[/]", $"Tu saldo actual es: {balance:N0} millas");

            // Mostrar a qué descuento aplica
            var maxTier = DiscountTiers.LastOrDefault(t => balance >= t.Miles);
            if (maxTier.Miles > 0)
            {
                SpectreUi.MarkupLineOrPlain(
                    $"[bold yellow]→ Puedes aplicar hasta {maxTier.Label}[/]",
                    $"→ Puedes aplicar hasta {maxTier.Label}"
                );
            }
            else
            {
                var nextTier = DiscountTiers[0];
                var remaining = nextTier.Miles - balance;
                SpectreUi.MarkupLineOrPlain(
                    $"[dim]Te faltan {remaining:N0} millas para tu primer descuento ({nextTier.Label}).[/]",
                    $"Te faltan {remaining:N0} millas para tu primer descuento ({nextTier.Label})."
                );
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        SpectreUi.Pause();
    }

    // ═══════════════════════════════════════════════════════════
    //  ACCIONES ADMIN / ROOT
    // ═══════════════════════════════════════════════════════════

    private void GetBalance()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar Saldo", null);
            var clientId = GetValidClientId();
            var balance = _service.GetClientBalanceAsync(clientId).Result;
            
            SpectreUi.MarkupLineOrPlain($"[green]Saldo actual del cliente {clientId}: {balance} millas[/]", $"Saldo actual del cliente {clientId}: {balance} millas");
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        SpectreUi.Pause();
    }

    private void SimulateAccumulation()
    {
        try
        {
            SpectreUi.ModuleHeader("Simular Acumulación", null);
            var clientId = GetValidClientId();
            var distance = decimal.Parse(SpectreUi.PromptRequiredCancelable("Distancia del vuelo (Km)"));
            
            _service.AccumulateMilesAsync(clientId, 1, distance * 10m).Wait(); // 1 como reservationId temporal, escala x10
            
            SpectreUi.MarkupLineOrPlain("[green]Millas acumuladas exitosamente.[/]", "Millas acumuladas exitosamente.");
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.Message}"); }
        SpectreUi.Pause();
    }

    private void SimulateRedemption()
    {
        try
        {
            SpectreUi.ModuleHeader("Simular Redención", null);
            var clientId = GetValidClientId();
            var miles = decimal.Parse(SpectreUi.PromptRequiredCancelable("Millas a redimir"));
            
            _service.RedeemMilesAsync(clientId, 1, miles).Wait(); // 1 como reservationId temporal
            
            SpectreUi.MarkupLineOrPlain("[green]Millas redimidas exitosamente.[/]", "Millas redimidas exitosamente.");
        }
        catch (OperationCanceledException) { }
        catch (Exception ex) { Console.WriteLine($"Error: {ex.InnerException?.Message ?? ex.Message}"); }
        SpectreUi.Pause();
    }

    // ═══════════════════════════════════════════════════════════
    //  REPORTES ANALÍTICOS (ADMIN)
    // ═══════════════════════════════════════════════════════════

    private async Task GetTopAccumulators()
    {
        var data = await _service.GetTopAccumulatorsAsync();
        SpectreUi.ModuleHeader("Reporte", "Top 10: Clientes con más millas acumuladas");
        if (data.Any())
        {
        SpectreUi.ShowTable("Top Acumuladores",
            ["ID Cliente", "Cliente", "Total Acumulado"],
            data.Select(d => (IReadOnlyList<string>)new[]
            {
                d.ClientId.ToString(),
                d.ClientName,
                d.TotalAccumulated.ToString("N0")
            }).ToList());
        }
        else Console.WriteLine("Sin datos.");
        SpectreUi.Pause();
    }

    private async Task GetTopRedeemers()
    {
        var data = await _service.GetTopRedeemersAsync();
        SpectreUi.ModuleHeader("Reporte", "Top 10: Clientes que más redimen");
        if (data.Any())
        {
        SpectreUi.ShowTable("Top Redentores",
            ["ID Cliente", "Cliente", "Total Redimido"],
            data.Select(d => (IReadOnlyList<string>)new[]
            {
                d.ClientId.ToString(),
                d.ClientName,
                d.TotalRedeemed.ToString("N0")
            }).ToList());
        }
        else Console.WriteLine("Sin datos.");
        SpectreUi.Pause();
    }

    private async Task GetTopAirlines()
    {
        var data = await _service.GetTopAirlinesByLoyaltyAsync();
        SpectreUi.ModuleHeader("Reporte", "Aerolíneas con mayor volumen de fidelización");
        if (data.Any())
        {
        SpectreUi.ShowTable("Aerolíneas",
            ["ID Aerolínea", "Aerolínea", "Millas Otorgadas"],
            data.Select(d => (IReadOnlyList<string>)new[]
            {
                d.AirlineId.ToString(),
                d.AirlineName,
                d.TotalMilesGranted.ToString("N0")
            }).ToList());
        }
        else Console.WriteLine("Sin datos.");
        SpectreUi.Pause();
    }

    private async Task GetTopRoutes()
    {
        var data = await _service.GetTopRoutesByMilesAsync();
        SpectreUi.ModuleHeader("Reporte", "Top 5: Rutas con mayor acumulación");
        if (data.Any())
        {
        SpectreUi.ShowTable("Rutas",
            ["Origen", "Destino", "Millas"],
            data.Select(d => (IReadOnlyList<string>)new[]
            {
                d.Origin,
                d.Destination,
                d.TotalMiles.ToString("N0")
            }).ToList());
        }
        else Console.WriteLine("Sin datos.");
        SpectreUi.Pause();
    }

    private async Task GetFrequentFlyers()
    {
        var data = await _service.GetFrequentFlyersRankingAsync();
        SpectreUi.ModuleHeader("Reporte", "Ranking de Viajeros Frecuentes");
        if (data.Any())
        {
        SpectreUi.ShowTable("Viajeros Frecuentes",
            ["ID Cliente", "Cliente", "Vuelos Completados", "Saldo de Millas"],
            data.Select(d => (IReadOnlyList<string>)new[]
            {
                d.ClientId.ToString(),
                d.ClientName,
                d.CompletedFlights.ToString(),
                d.CurrentBalance.ToString("N0")
            }).ToList());
        }
        else Console.WriteLine("Sin datos.");
        SpectreUi.Pause();
    }

    // ═══════════════════════════════════════════════════════════
    //  HELPERS
    // ═══════════════════════════════════════════════════════════

    private int GetValidClientId()
    {
        while (true)
        {
            var clientId = SpectreUi.PromptIntRequiredCancelable("ID del Cliente", min: 1);
            if (_service.ClientExistsAsync(clientId).Result)
            {
                return clientId;
            }
            SpectreUi.MarkupLineOrPlain(
                "[red]El cliente no existe. Por favor ingrese un ID de cliente existente.[/]", 
                "El cliente no existe. Por favor ingrese un ID de cliente existente."
            );
        }
    }
}

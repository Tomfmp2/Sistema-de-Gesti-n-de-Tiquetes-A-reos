using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationPassengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Passengers.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Persons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationFlights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Tickets.UI;

public sealed class TicketConsoleUI : IModuleUI
{
    private readonly CreateTicketUseCase _create;
    private readonly GetTicketByIdUseCase _getById;
    private readonly GetAllTicketsUseCase _getAll;
    private readonly UpdateTicketUseCase _update;
    private readonly DeleteTicketUseCase _delete;
    private readonly AppDbContext _ctx;

    public TicketConsoleUI(
        CreateTicketUseCase create,
        GetTicketByIdUseCase getById,
        GetAllTicketsUseCase getAll,
        UpdateTicketUseCase update,
        DeleteTicketUseCase delete,
        AppDbContext ctx
    )
    {
        _create = create;
        _getById = getById;
        _getAll = getAll;
        _update = update;
        _delete = delete;
        _ctx = ctx;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Tickets", "Emisión y gestión de tickets");
            var items = new (string Label, Action Action)[]
            {
                ("Emitir ticket", () => Create().GetAwaiter().GetResult()),
                ("Listar todos", () => ListAll().GetAwaiter().GetResult()),
                ("Consultar por ID", () => GetById().GetAwaiter().GetResult()),
                ("Actualizar", () => Update().GetAwaiter().GetResult()),
                ("Eliminar", () => Delete().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };
            MenuLogic.RunMenu(items);
        }
    }

    private async Task Create()
    {
        try
        {
            SpectreUi.ModuleHeader("Emitir ticket", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var reservationPassengerId = SpectreUi.PromptIntRequiredCancelable("ReservationPassengerId", min: 1);
            var code = SpectreUi.PromptRequiredCancelable("Código", "p.ej. TKT-ABC123");
            var issueDate = PromptDateTimeRequired("Fecha emisión (UTC)", "yyyy-MM-dd HH:mm");
            var ticketStatusId = SpectreUi.PromptIntRequiredCancelable("TicketStatusId", min: 1);
            var now = DateTime.UtcNow;

            var created = await _create.ExecuteAsync(new CreateTicketRequest(
                ReservationPassengerId: reservationPassengerId,
                Code: code,
                IssueDate: issueDate,
                TicketStatusId: ticketStatusId,
                CreatedAt: now,
                UpdatedAt: now
            ));

            SpectreUi.MarkupLineOrPlain(
                $"[green]Ticket creado[/] id={created.Id.Value} code=[bold]{created.Code.Value}[/]",
                $"Ticket creado id={created.Id.Value} code={created.Code.Value}"
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task ListAll()
    {
        try
        {
            var list = (await _getAll.ExecuteAsync()).ToList();
            if (list.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No hay tickets para mostrar.[/]", "No hay tickets para mostrar.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ModuleHeader("Tickets", "Listado");
            SpectreUi.ShowTable(
                "Tickets",
                ["ID", "ReservationPassengerId", "Code", "IssueDate", "StatusId"],
                list.OrderBy(x => x.Id.Value).Select(x => (IReadOnlyList<string>)new[]
                {
                    x.Id.Value.ToString(),
                    x.ReservationPassengerId.Value.ToString(),
                    x.Code.Value,
                    x.IssueDate.Value.ToString("yyyy-MM-dd HH:mm"),
                    x.TicketStatusId.Value.ToString()
                }).ToList()
            );
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task GetById()
    {
        try
        {
            SpectreUi.ModuleHeader("Consultar tiquete", null);
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            
            var ticket = await _ctx.Set<TicketEntity>()
                .AsNoTracking()
                .Include(t => t.ReservationPassenger)
                    .ThenInclude(rp => rp!.Passenger)
                        .ThenInclude(p => p!.Person)
                .Include(t => t.ReservationPassenger)
                    .ThenInclude(rp => rp!.FlightSeat)
                        .ThenInclude(s => s!.CabinType)
                .Include(t => t.TicketStatus)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket is null)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No encontrado.[/]", "No encontrado.");
                SpectreUi.Pause();
                return;
            }

            var rp = ticket.ReservationPassenger;
            var person = rp?.Passenger?.Person;
            var seat = rp?.FlightSeat;

            // Buscar el vuelo y la tarifa parcial
            var flightInfo = await _ctx.Set<ReservationFlightEntity>()
                .AsNoTracking()
                .Include(rf => rf.Flight)
                .Where(rf => rf.Id == (rp != null ? rp.ReservationFlightId : 0))
                .Select(rf => new { rf.Flight!.FlightCode, rf.PartialValue })
                .FirstOrDefaultAsync();

            SpectreUi.ShowTable(
                "Detalles del Tiquete",
                ["Campo", "Valor"],
                [
                    ["ID Interno", ticket.Id.ToString()],
                    ["Código Tiquete", $"[bold cyan]{ticket.Code}[/]"],
                    ["Estado", ticket.TicketStatus?.Name ?? ticket.TicketStatusId.ToString()],
                    ["Fecha Emisión", ticket.IssueDate.ToString("yyyy-MM-dd HH:mm")],
                    ["─── Pasajero ───", ""],
                    ["Nombre", $"{person?.FirstName} {person?.LastName}"],
                    ["Documento", $"{person?.DocumentNumber}"],
                    ["─── Vuelo y Asiento ───", ""],
                    ["Vuelo", flightInfo?.FlightCode ?? "N/A"],
                    ["Asiento", seat != null ? $"[bold yellow]{seat.SeatCode}[/] ({seat.CabinType?.Name})" : "[grey]No asignado[/]"],
                    ["Tarifa Pagada", flightInfo != null ? $"[green]${flightInfo.PartialValue:0.00}[/]" : "N/A"],
                ]
            );
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task Update()
    {
        try
        {
            SpectreUi.ModuleHeader("Actualizar ticket", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para salir sin guardar.[/]",
                "Tip: escriba 0 / c / cancelar para salir sin guardar."
            );

            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            var reservationPassengerId = SpectreUi.PromptIntRequiredCancelable("ReservationPassengerId", min: 1);
            var code = SpectreUi.PromptRequiredCancelable("Código", "p.ej. TKT-ABC123");
            var issueDate = PromptDateTimeRequired("Fecha emisión (UTC)", "yyyy-MM-dd HH:mm");
            var ticketStatusId = SpectreUi.PromptIntRequiredCancelable("TicketStatusId", min: 1);
            var now = DateTime.UtcNow;

            await _update.ExecuteAsync(new UpdateTicketRequest(
                Id: id,
                ReservationPassengerId: reservationPassengerId,
                Code: code,
                IssueDate: issueDate,
                TicketStatusId: ticketStatusId,
                CreatedAt: now,
                UpdatedAt: now
            ));

            SpectreUi.MarkupLineOrPlain("[green]Actualizado.[/]", "Actualizado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private async Task Delete()
    {
        try
        {
            SpectreUi.ModuleHeader("Eliminar ticket", null);
            SpectreUi.MarkupLineOrPlain(
                "[grey]Tip: escriba 0 / c / cancelar para volver.[/]",
                "Tip: escriba 0 / c / cancelar para volver."
            );
            var id = SpectreUi.PromptIntRequiredCancelable("ID", min: 1);
            await _delete.ExecuteAsync(id);
            SpectreUi.MarkupLineOrPlain("[green]Eliminado.[/]", "Eliminado.");
        }
        catch (OperationCanceledException)
        {
            SpectreUi.MarkupLineOrPlain("[grey]Operación cancelada.[/]", "Operación cancelada.");
        }
        catch (Exception ex)
        {
            SpectreUi.ShowException(ex);
        }
        SpectreUi.Pause();
    }

    private static DateTime PromptDateTimeRequired(string label, string help)
    {
        while (true)
        {
            var raw = SpectreUi.PromptRequiredCancelable(label, help);
            if (DateTime.TryParse(raw, out var dt))
                return dt;
            SpectreUi.MarkupLineOrPlain("[red]Fecha/hora inválida.[/]", "Fecha/hora inválida.");
        }
    }
}


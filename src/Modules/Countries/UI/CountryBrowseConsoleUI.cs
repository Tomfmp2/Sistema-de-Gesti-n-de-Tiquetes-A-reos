using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Ui;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Countries.UI;

public sealed class CountryBrowseConsoleUI : IModuleUI
{
    private readonly AppDbContext _ctx;

    public CountryBrowseConsoleUI(AppDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task RunAsync()
    {
        var exit = false;
        while (!exit)
        {
            SpectreUi.ModuleHeader("Países", "Buscar por continente y ver IDs (Name / ISO / Id)");

            var items = new List<(string Label, Action Action)>
            {
                ("Buscar por continente", () => BrowseByContinent().GetAwaiter().GetResult()),
                ("Buscar por texto (rápido)", () => SearchText().GetAwaiter().GetResult()),
                ("Volver", () => exit = true),
            };

            MenuLogic.RunMenu(
                items,
                "[bold]¿Qué deseas hacer?[/]",
                "[grey]Sugerido: «Buscar por continente» para ubicar el Id del país.[/]"
            );
        }
    }

    private async Task BrowseByContinent()
    {
        try
        {
            SpectreUi.ModuleHeader("Países por continente", "1) Elige un continente · 2) Filtra y lista países");

            var continents = await _ctx.Set<ContinentEntity>()
                .AsNoTracking()
                .OrderBy(c => c.Id)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            if (continents.Count == 0)
                throw new InvalidOperationException("No hay continentes registrados.");

            SpectreUi.ShowTable(
                "Continentes",
                ["Id", "Nombre"],
                continents.Select(c => (IReadOnlyList<string>)new[]
                {
                    c.Id.ToString(),
                    c.Name ?? "-"
                }).ToList()
            );

            var continentId = SpectreUi.PromptIntRequiredCancelable("ContinentId", "Escribe el Id del continente", min: 1);
            if (continents.All(c => c.Id != continentId))
                throw new InvalidOperationException("ContinentId inválido.");

            var filter = (SpectreUi.PromptOptionalCancelable(
                "Filtro (opcional)",
                "Nombre o ISO (ej: col / CO). Enter = sin filtro"
            ) ?? string.Empty).Trim();

            var q = _ctx.Set<CountryEntity>()
                .AsNoTracking()
                .Where(p => p.ContinentId == continentId);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                var f = filter.Trim();
                q = q.Where(p =>
                    (p.Name != null && EF.Functions.Like(p.Name, $"%{f}%"))
                    || (p.CodeIso != null && EF.Functions.Like(p.CodeIso, $"%{f}%"))
                );
            }

            var countries = await q
                .OrderBy(p => p.Name)
                .Select(p => new { p.Id, p.Name, p.CodeIso })
                .ToListAsync();

            if (countries.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain(
                    "[grey]No se encontraron países para ese continente/filtro.[/]",
                    "No se encontraron países para ese continente/filtro."
                );
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                $"Países (continent_id={continentId})",
                ["Id", "Nombre", "ISO"],
                countries.Select(p => (IReadOnlyList<string>)new[]
                {
                    p.Id.ToString(),
                    p.Name ?? "-",
                    p.CodeIso ?? "-"
                }).ToList()
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

    private async Task SearchText()
    {
        try
        {
            SpectreUi.ModuleHeader("Búsqueda rápida de países", "Encuentra Id por nombre/ISO sin escoger continente");
            var text = SpectreUi.PromptRequiredCancelable("Texto", "Nombre o ISO (ej: usa / US / col)");

            var list = await _ctx.Set<CountryEntity>()
                .AsNoTracking()
                .Where(p =>
                    (p.Name != null && EF.Functions.Like(p.Name, $"%{text}%"))
                    || (p.CodeIso != null && EF.Functions.Like(p.CodeIso, $"%{text}%"))
                )
                .OrderBy(p => p.Name)
                .Take(80)
                .Select(p => new { p.Id, p.Name, p.CodeIso, p.ContinentId })
                .ToListAsync();

            if (list.Count == 0)
            {
                SpectreUi.MarkupLineOrPlain("[grey]No se encontraron coincidencias.[/]", "No se encontraron coincidencias.");
                SpectreUi.Pause();
                return;
            }

            SpectreUi.ShowTable(
                "Países (máx 80)",
                ["Id", "Nombre", "ISO", "ContinentId"],
                list.Select(p => (IReadOnlyList<string>)new[]
                {
                    p.Id.ToString(),
                    p.Name ?? "-",
                    p.CodeIso ?? "-",
                    p.ContinentId.ToString()
                }).ToList()
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
}


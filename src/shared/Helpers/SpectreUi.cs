using Spectre.Console;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

/// <summary>
/// Presentación de consola con Spectre (títulos, selección, mensajes en español).
/// </summary>
public static class SpectreUi
{
    private readonly record struct MenuEntry(string Key, string Label);

    public static void Clear() => AnsiConsole.Clear();

    public static void ShowAppBanner()
    {
        AnsiConsole.Write(new FigletText("Tiquetes").LeftJustified().Color(Color.Cyan1));

        AnsiConsole.MarkupLine("[grey]Sistema de gestión de tiquetes aéreos[/]");
        AnsiConsole.Write(new Rule("[bold green]Menú principal[/]").RuleStyle("grey"));
    }

    public static void ModuleHeader(string title, string? subtitle = null)
    {
        AnsiConsole.Clear();
        var panel = new Panel(
            subtitle is null
                ? new Markup($"[bold white]{title}[/]")
                : new Markup($"[bold white]{title}[/]\n[grey]{subtitle}[/]")
        )
            .Border(BoxBorder.Rounded)
            .BorderColor(Color.Green);
        AnsiConsole.Write(panel);
    }

    /// <summary>
    /// Devuelve la clave numérica asociada (p. ej. "1", "0"), no el texto completo.
    /// </summary>
    public static string PromptMenuKey(
        string promptTitle,
        IReadOnlyList<(string Key, string Label)> options
    )
    {
        var entries = options.Select(o => new MenuEntry(o.Key, o.Label)).ToList();
        var prompt = new SelectionPrompt<MenuEntry>()
            .Title($"[bold]{promptTitle}[/]")
            .PageSize(15)
            .MoreChoicesText("[grey](Más opciones abajo)[/]")
            .HighlightStyle(new Style(foreground: Color.Lime, decoration: Decoration.Bold))
            .UseConverter(e => $"[yellow]{e.Key}[/] — {e.Label}");

        prompt.AddChoices(entries);
        MenuEntry selected = AnsiConsole.Prompt(prompt);
        return selected.Key;
    }

    public static void Pause(string message = "Pulse una tecla para continuar…")
    {
        AnsiConsole.MarkupLine($"[grey]{message}[/]");
        System.Console.ReadKey(true);
    }

    public static void ShowInvalidOption()
    {
        AnsiConsole.MarkupLine("[red]Opción no válida.[/]");
        Pause();
    }

    public static void ShowException(Exception ex)
    {
        AnsiConsole.WriteException(ex);
        Pause();
    }
}

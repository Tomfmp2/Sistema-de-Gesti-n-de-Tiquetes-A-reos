using System.Text;
using Spectre.Console;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

/// <summary>
/// Presentación de consola con Spectre (títulos, selección, mensajes en español).
/// En terminales sin consola interactiva real (pipes, algunos hosts), usa modo texto plano.
/// </summary>
public static class SpectreUi
{
    private readonly record struct MenuEntry(string Key, string Label);

    /// <summary>
    /// Si es false, no se usan prompts interactivos de Spectre (evita IOException / “Controlador no válido” en Windows).
    /// </summary>
    public static bool CanUseAnsiPrompts { get; private set; }

    /// <summary>
    /// Llamar una vez al inicio de la aplicación, antes de cualquier prompt.
    /// </summary>
    public static void InitializeConsoleUi()
    {
        if (string.Equals(Environment.GetEnvironmentVariable("PLAIN_CONSOLE"), "1", StringComparison.Ordinal))
        {
            CanUseAnsiPrompts = false;
            return;
        }

        try
        {
            if (OperatingSystem.IsWindows())
            {
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;
            }
        }
        catch
        {
            // ignorar: codificaciones opcionales
        }

        if (!Environment.UserInteractive || Console.IsOutputRedirected || Console.IsInputRedirected)
        {
            CanUseAnsiPrompts = false;
            return;
        }

        try
        {
            _ = Console.BufferWidth;
            _ = Console.KeyAvailable;
        }
        catch (IOException)
        {
            CanUseAnsiPrompts = false;
            return;
        }
        catch (InvalidOperationException)
        {
            CanUseAnsiPrompts = false;
            return;
        }

        try
        {
            AnsiConsole.Console = AnsiConsole.Create(
                new AnsiConsoleSettings
                {
                    Ansi = AnsiSupport.Yes,
                    ColorSystem = ColorSystemSupport.Standard,
                    Out = new AnsiConsoleOutput(Console.Out),
                }
            );
        }
        catch
        {
            // mantener instancia por defecto
        }

        CanUseAnsiPrompts = true;
    }

    /// <summary>
    /// Desactiva Spectre interactivo tras un fallo en tiempo de ejecución (p. ej. handle inválido).
    /// </summary>
    public static void DisableRichConsole() => CanUseAnsiPrompts = false;

    public static void Clear()
    {
        if (!CanUseAnsiPrompts)
        {
            TryPlainClear();
            return;
        }

        try
        {
            AnsiConsole.Clear();
        }
        catch (IOException)
        {
            DisableRichConsole();
            TryPlainClear();
        }
        catch (InvalidOperationException)
        {
            DisableRichConsole();
            TryPlainClear();
        }
    }

    private static void TryPlainClear()
    {
        try
        {
            if (!Console.IsOutputRedirected)
                Console.Clear();
        }
        catch
        {
            // sin consola física
        }
    }

    public static void ShowAppBanner()
    {
        if (!CanUseAnsiPrompts)
        {
            Console.WriteLine("=== Tiquetes — Sistema de gestión de tiquetes aéreos ===");
            Console.WriteLine("--- Menú principal ---");
            return;
        }

        try
        {
            AnsiConsole.Write(new FigletText("Tiquetes").LeftJustified().Color(Color.Cyan1));
            AnsiConsole.MarkupLine("[grey]Sistema de gestión de tiquetes aéreos[/]");
            AnsiConsole.Write(new Rule("[bold green]Menú principal[/]").RuleStyle("grey"));
        }
        catch
        {
            DisableRichConsole();
            Console.WriteLine("=== Tiquetes — Sistema de gestión de tiquetes aéreos ===");
            Console.WriteLine("--- Menú principal ---");
        }
    }

    public static void ModuleHeader(string title, string? subtitle = null)
    {
        if (!CanUseAnsiPrompts)
        {
            Console.WriteLine();
            Console.WriteLine(subtitle is null ? $"=== {title} ===" : $"=== {title} — {subtitle} ===");
            Console.WriteLine();
            return;
        }

        try
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
        catch
        {
            DisableRichConsole();
            Console.WriteLine();
            Console.WriteLine(subtitle is null ? $"=== {title} ===" : $"=== {title} — {subtitle} ===");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Devuelve la clave numérica asociada (p. ej. "1", "0"), no el texto completo.
    /// </summary>
    public static string PromptMenuKey(string promptTitle, IReadOnlyList<(string Key, string Label)> options)
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

    public static void MarkupLineOrPlain(string markup, string plain)
    {
        if (!CanUseAnsiPrompts)
        {
            Console.WriteLine(plain);
            return;
        }

        try
        {
            AnsiConsole.MarkupLine(markup);
        }
        catch
        {
            DisableRichConsole();
            Console.WriteLine(plain);
        }
    }

    public static void Pause(string message = "Pulse una tecla para continuar…")
    {
        if (!CanUseAnsiPrompts)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            return;
        }

        try
        {
            AnsiConsole.MarkupLine($"[grey]{message}[/]");
            Console.ReadKey(true);
        }
        catch (IOException)
        {
            DisableRichConsole();
            Console.WriteLine(message);
            Console.ReadLine();
        }
        catch (InvalidOperationException)
        {
            DisableRichConsole();
            Console.WriteLine(message);
            Console.ReadLine();
        }
    }

    public static void ShowInvalidOption()
    {
        MarkupLineOrPlain("[red]Opción no válida.[/]", "Opción no válida.");
        Pause();
    }

    public static void ShowException(Exception ex)
    {
        if (!CanUseAnsiPrompts)
        {
            Console.WriteLine(ex.ToString());
            Pause();
            return;
        }

        try
        {
            AnsiConsole.WriteException(ex);
            Pause();
        }
        catch
        {
            DisableRichConsole();
            Console.WriteLine(ex.ToString());
            Pause();
        }
    }
}

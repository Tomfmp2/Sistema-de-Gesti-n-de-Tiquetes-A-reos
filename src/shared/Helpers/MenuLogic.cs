using Spectre.Console;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

public static class MenuLogic
{
    private readonly record struct MenuOption(string Label, Action Action);

    /// <summary>
    /// Menú con flechas arriba/abajo y Enter para confirmar (Spectre.Console).
    /// </summary>
    /// <param name="items">Opciones en el orden mostrado.</param>
    /// <param name="title">Título del prompt; si es null, se usa un texto por defecto en español.</param>
    public static void RunMenu(
        IReadOnlyList<(string Label, Action Action)> items,
        string? title = null
    )
    {
        ArgumentNullException.ThrowIfNull(items);
        if (items.Count == 0)
            return;

        if (!SpectreUi.CanUseAnsiPrompts)
        {
            RunMenuPlain(items, title);
            return;
        }

        try
        {
            var choices = items.Select(i => new MenuOption(i.Label, i.Action)).ToList();
            var prompt = new SelectionPrompt<MenuOption>()
                .Title(title ?? "[bold]Opciones[/]\n[grey]↑/↓ para moverse · Enter para elegir[/]")
                .PageSize(15)
                .MoreChoicesText("[grey](Más opciones abajo)[/]")
                .HighlightStyle(new Style(foreground: Color.Lime, decoration: Decoration.Bold))
                .UseConverter(o => o.Label);

            prompt.AddChoices(choices);
            MenuOption selected = AnsiConsole.Prompt(prompt);
            try
            {
                selected.Action();
            }
            catch (Exception ex)
            {
                SpectreUi.ShowException(ex);
            }
        }
        catch (IOException)
        {
            SpectreUi.DisableRichConsole();
            RunMenuPlain(items, title);
        }
        catch (InvalidOperationException)
        {
            SpectreUi.DisableRichConsole();
            RunMenuPlain(items, title);
        }
    }

    /// <summary>
    /// Selección por tecla numérica (sin eco). Preferir <see cref="RunMenu"/> para navegación con flechas.
    /// </summary>
    public static void Menus_logic(Dictionary<string, Action> selections)
    {
        ConsoleKeyInfo userSelection = Console.ReadKey(true);
        string key = userSelection.KeyChar.ToString();

        if (selections.TryGetValue(key, out Action? action))
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Console.Write("\nOprima cualquier tecla para continuar...");
                Console.ReadKey(true);
            }
        }
        else
        {
            Console.Write("\nOpción no válida, oprima cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Ejecuta la acción asociada a una clave (p. ej. tras un <c>SelectionPrompt</c> de Spectre.Console).
    /// </summary>
    public static bool TryInvoke(IReadOnlyDictionary<string, Action> selections, string key)
    {
        if (selections.TryGetValue(key, out Action? action))
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                SpectreUi.ShowException(ex);
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Variante asíncrona para submenús cuyas operaciones son <c>Task</c>.
    /// </summary>
    public static async Task<bool> TryInvokeAsync(
        IReadOnlyDictionary<string, Func<Task>> selections,
        string key
    )
    {
        if (selections.TryGetValue(key, out Func<Task>? action))
        {
            try
            {
                await action();
                return true;
            }
            catch (Exception ex)
            {
                SpectreUi.ShowException(ex);
                return true;
            }
        }

        return false;
    }

    private static void RunMenuPlain(IReadOnlyList<(string Label, Action Action)> items, string? title)
    {
        Console.WriteLine();
        Console.WriteLine(title is null ? "Opciones (número + Enter):" : StripMarkupTitle(title));
        for (var i = 0; i < items.Count; i++)
            Console.WriteLine($"  {i + 1}. {items[i].Label}");

        Console.Write("Elija una opción: ");
        var line = Console.ReadLine();
        if (!int.TryParse(line, out var n) || n < 1 || n > items.Count)
        {
            Console.WriteLine("Opción no válida.");
            Console.WriteLine("Pulse Enter para continuar…");
            Console.ReadLine();
            return;
        }

        try
        {
            items[n - 1].Action();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Console.WriteLine("Pulse Enter para continuar…");
            Console.ReadLine();
        }
    }

    private static string StripMarkupTitle(string title)
    {
        var t = title;
        var i = 0;
        while (i < t.Length)
        {
            var open = t.IndexOf('[', i);
            if (open < 0)
                break;
            var close = t.IndexOf(']', open);
            if (close < 0)
                break;
            t = t.Remove(open, close - open + 1);
            i = open;
        }

        return string.IsNullOrWhiteSpace(t) ? "Opciones:" : t.Trim();
    }
}

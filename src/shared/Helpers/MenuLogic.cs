namespace sistema_gestor_de_tiquetes_aereos.Src.shared.Helpers;

public class MenuLogic
{
    public static void Menus_logic(Dictionary<string, Action> Selections)
    {
        ConsoleKeyInfo user_selection = Console.ReadKey(true);

        string key = user_selection.KeyChar.ToString();

        if (Selections.ContainsKey(key))
            Selections[key]();
        else
        {
            Console.Write("\nOpcion no valida, oprima cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}

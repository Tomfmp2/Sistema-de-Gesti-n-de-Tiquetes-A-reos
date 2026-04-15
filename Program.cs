using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

try
{
    var context = DbContextFactory.Create();

    if (context.Database.CanConnect())
    {
        Console.WriteLine("Conexion exitosa");
    }
    else
    {
        Console.WriteLine("No se pudo conectar a la base de datos");
    }
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Error al conectar con la base de datos: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.Error.WriteLine($"Detalle: {ex.InnerException.Message}");
    }
}

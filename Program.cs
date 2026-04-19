using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;

try
{
    var context = DbContextFactory.Create();

    if (context.Database.CanConnect())
    {
        Console.WriteLine("Conexion exitosa");
        // Aplicar migraciones automáticamente para asegurar que la BD esté actualizada
        context.Database.Migrate();
        Console.WriteLine("Migraciones aplicadas correctamente");
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

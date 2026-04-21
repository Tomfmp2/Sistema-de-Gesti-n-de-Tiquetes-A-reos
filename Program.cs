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
        
        // Test FK relationships
        Console.WriteLine("\nTesting FK relationships...");
        
        // Test 1: Check if we can query continents
        var continents = context.Continents.ToList();
        Console.WriteLine($"Found {continents.Count} continents");
        
        // Test 2: Check if we can query countries with FK to continents
        var countries = context.Countries.Include(c => c.Continent).ToList();
        Console.WriteLine($"Found {countries.Count} countries with continent relationships");
        
        // Test 3: Check if we can query regions with FK to countries
        var regions = context.Regions.Include(r => r.Country).ToList();
        Console.WriteLine($"Found {regions.Count} regions with country relationships");
        
        Console.WriteLine("FK relationships are working correctly!");
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

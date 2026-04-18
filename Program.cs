using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Infrastructure.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.BaggageTypes.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Application.UseCases;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Infrastructure.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Baggage.UI;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

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

    // Configurar Service Provider
    var services = new ServiceCollection();
    services.AddScoped<AppDbContext>(_ => context);

    // Registrar BaggageTypes Module
    services.AddScoped<IBaggageTypeRepository, BaggageTypeRepository>();
    services.AddScoped<CreateBaggageTypeUseCase>();
    services.AddScoped<GetBaggageTypeByIdUseCase>();
    services.AddScoped<GetAllBaggageTypesUseCase>();
    services.AddScoped<UpdateBaggageTypeUseCase>();
    services.AddScoped<DeleteBaggageTypeUseCase>();
    services.AddScoped<BaggageTypeConsoleUI>();

    // Registrar Baggage Module
    services.AddScoped<IBaggageRepository, BaggageRepository>();
    services.AddScoped<CreateBaggageUseCase>();
    services.AddScoped<GetBaggageByIdUseCase>();
    services.AddScoped<GetAllBaggagesUseCase>();
    services.AddScoped<UpdateBaggageUseCase>();
    services.AddScoped<DeleteBaggageUseCase>();
    services.AddScoped<BaggageConsoleUI>();

    var serviceProvider = services.BuildServiceProvider();

    // Resolver y ejecutar UI
    Console.WriteLine("\n╔═══════════════════════════════════════╗");
    Console.WriteLine("║  SISTEMA GESTIÓN DE TIQUETES AÉREOS  ║");
    Console.WriteLine("╚═══════════════════════════════════════╝");
    Console.WriteLine("\nSeleccione módulo:");
    Console.WriteLine("1. Baggage Types");
    Console.WriteLine("2. Baggage");
    Console.WriteLine("0. Salir");
    Console.Write("\nOpción: ");

    string? option = Console.ReadLine();
    switch (option)
    {
        case "1":
            var baggageTypeUI = serviceProvider.GetRequiredService<BaggageTypeConsoleUI>();
            await baggageTypeUI.RunAsync();
            break;
        case "2":
            var baggageUI = serviceProvider.GetRequiredService<BaggageConsoleUI>();
            await baggageUI.RunAsync();
            break;
        case "0":
            Console.WriteLine("Saliendo...");
            break;
        default:
            Console.WriteLine("Opción inválida");
            break;
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

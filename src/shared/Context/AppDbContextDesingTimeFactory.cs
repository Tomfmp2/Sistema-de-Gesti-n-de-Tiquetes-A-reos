using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using MyInventory2026.src.shared.helpers;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace MyInventory2026.src.shared.context;

public sealed class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory();

        var config = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsetting.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString =
            Environment.GetEnvironmentVariable("MYSQL_CONNECTION")
            ?? config.GetConnectionString("MySqlDB");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("No se encontró una cadena de conexión válida.");
        }

        var detectedVersion = MySqlVersionResolver.DetectVersion(connectionString);
        var minVersion = new Version(8, 0, 0);

        if (detectedVersion < minVersion)
        {
            throw new NotSupportedException(
                $"Versión de MySQL no soportada: {detectedVersion}. Requiere {minVersion} o superior."
            );
        }

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(connectionString, new MySqlServerVersion(detectedVersion))
            .Options;

        return new AppDbContext(options);
    }
}

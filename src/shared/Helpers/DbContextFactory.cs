using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Helpers;

public class DbContextFactory
{
    public static AppDbContext Create()
    {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables()
            .Build();

        string? ConnectionString =
            Environment.GetEnvironmentVariable("MYSQL_CONNECTION")
            ?? config.GetConnectionString("MySqlDB");

        if (string.IsNullOrWhiteSpace(ConnectionString))
        {
            throw new InvalidOperationException("No connection string found");
        }

        // Si la BD está caída, la detección automática puede fallar.
        // En ese caso usamos una versión "razonable" para permitir que la app
        // llegue al login y muestre el error de conexión de forma amigable.
        Version detectedVersion;
        try
        {
            detectedVersion = MySqlVersionResolver.DetectVersion(ConnectionString);
        }
        catch
        {
            detectedVersion = new Version(8, 0, 0);
        }

        var minVersion = new Version(8, 0, 0);
        if (detectedVersion < minVersion)
        {
            throw new NotSupportedException(
                $"Versión de MySQL no soportada: {detectedVersion}. Requiere {minVersion} o superior."
            );
        }

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseMySql(
                ConnectionString,
                new MySqlServerVersion(detectedVersion),
                mySqlOptions =>
                {
                    // Resiliencia ante fallos transitorios de red/conexión (muy común en MySQL local/SSL).
                    mySqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(2),
                        errorNumbersToAdd: null
                    );
                    mySqlOptions.CommandTimeout(10);
                }
            )
            .Options;
        return new AppDbContext(options);
    }
}

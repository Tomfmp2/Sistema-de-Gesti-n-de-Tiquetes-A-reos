namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Clients.Infrastructure.Data;

public static class ClientDefaultData
{
    private static readonly DateTime SeedTimestamp = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public const int CatalogPersonDocumentTypeId = 1;

    /// <summary>Persona catálogo cliente (misma fila que en <c>PersonDefaultData</c>).</summary>
    public const string CatalogPersonDocumentNumber = "1098765432";

    public static DateTime CatalogCreatedAt => SeedTimestamp;
}

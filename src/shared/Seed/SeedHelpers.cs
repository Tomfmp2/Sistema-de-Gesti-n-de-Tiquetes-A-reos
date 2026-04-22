namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

public static class SeedHelpers
{
    public static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        return value.Trim().ToUpperInvariant();
    }
}


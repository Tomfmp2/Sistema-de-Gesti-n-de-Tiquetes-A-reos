namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Seed;

public static class SeedHelpers
{
    public static string Normalize(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        // Normalización estable para comparar textos con/sin tildes (evita duplicados "Nino" vs "Niño").
        var normalized = value.Trim().Normalize(System.Text.NormalizationForm.FormD);
        var chars = normalized.Where(c =>
            System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c)
            != System.Globalization.UnicodeCategory.NonSpacingMark);

        return new string(chars.ToArray()).Normalize(System.Text.NormalizationForm.FormC).ToUpperInvariant();
    }
}


namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Continents.Infrastructure.Seed;

public static class SeedHelpers
{
    public static string Normalize(string? value)
    {
        var normalized = (value ?? string.Empty).Trim().Normalize(System.Text.NormalizationForm.FormD);
        var chars = normalized.Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark);

        return new string(chars.ToArray()).Normalize(System.Text.NormalizationForm.FormC).ToUpperInvariant();
    }
}

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SystemRoles.Domain.ValueObjet;

public sealed class SystemRoleName
{
    public string Value { get; }

    public SystemRoleName(string value) => Value = value;

    public static SystemRoleName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 50)
        {
            throw new ArgumentException("Máximo 50 caracteres.");
        }

        return new SystemRoleName(t);
    }
}

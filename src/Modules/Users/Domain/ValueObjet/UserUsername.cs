namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

public sealed class UserUsername
{
    public string Value { get; }

    public UserUsername(string value) => Value = value;

    public static UserUsername Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El nombre de usuario no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 50)
        {
            throw new ArgumentException("Máximo 50 caracteres.");
        }

        return new UserUsername(t);
    }
}

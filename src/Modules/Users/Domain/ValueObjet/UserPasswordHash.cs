namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Users.Domain.ValueObjet;

public sealed class UserPasswordHash
{
    public string Value { get; }

    public UserPasswordHash(string value) => Value = value;

    public static UserPasswordHash Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El hash de contraseña no puede estar vacío.");
        }

        var t = value.Trim();
        if (t.Length > 255)
        {
            throw new ArgumentException("Máximo 255 caracteres.");
        }

        return new UserPasswordHash(t);
    }
}

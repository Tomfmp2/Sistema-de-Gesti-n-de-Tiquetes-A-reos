namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

public sealed class EmailDomainHost
{
    public string Value { get; }

    public EmailDomainHost(string value) => Value = value;

    public static EmailDomainHost Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("El dominio no puede estar vacío.");
        }

        var t = value.Trim().ToLowerInvariant();
        if (t.Length > 100)
        {
            throw new ArgumentException("Máximo 100 caracteres.");
        }

        return new EmailDomainHost(t);
    }
}

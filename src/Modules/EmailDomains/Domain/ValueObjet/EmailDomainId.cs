namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

public sealed class EmailDomainId
{
    public int Value { get; }

    public EmailDomainId(int value) => Value = value;

    public static EmailDomainId Unpersisted => new(0);

    public static EmailDomainId Create(int value)
    {
        if (value < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(value));
        }

        return new EmailDomainId(value);
    }
}

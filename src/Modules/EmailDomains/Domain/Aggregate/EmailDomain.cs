using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Domain.Aggregate;

public sealed class EmailDomain
{
    public EmailDomainId Id { get; private set; }
    public EmailDomainHost Domain { get; private set; }

    private EmailDomain(EmailDomainId id, EmailDomainHost domain)
    {
        Id = id;
        Domain = domain;
    }

    public static EmailDomain CreateNew(EmailDomainHost domain)
    {
        return new EmailDomain(EmailDomainId.Unpersisted, domain);
    }

    public static EmailDomain Create(EmailDomainId id, EmailDomainHost domain)
    {
        return new EmailDomain(id, domain);
    }
}

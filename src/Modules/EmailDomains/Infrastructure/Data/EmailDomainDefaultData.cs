using sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.EmailDomains.Infrastructure.Data;

public static class EmailDomainDefaultData
{
    public static readonly EmailDomainEntity[] EmailDomains =
    [
        new() { Id = 1, Domain = "gmail.com" },
        new() { Id = 2, Domain = "outlook.com" },
        new() { Id = 3, Domain = "hotmail.com" },
        new() { Id = 4, Domain = "yahoo.com" },
        new() { Id = 5, Domain = "icloud.com" },
        new() { Id = 6, Domain = "proton.me" }
    ];
}

using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Infrastructure.Entity;

public class CabinTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public static CabinTypeEntity FromDomain(Aggregate.CabinType cabinType)
    {
        return new CabinTypeEntity
        {
            Id = cabinType.Id.Value,
            Name = cabinType.Name.Value
        };
    }

    public Aggregate.CabinType ToDomain()
    {
        return Aggregate.CabinType.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject.CabinTypeId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinTypes.Domain.ValueObject.CabinTypeName.Reconstitute(Name)
        );
    }
}
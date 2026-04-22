using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightSeats.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;

public class SeatLocationTypeEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<FlightSeatEntity> FlightSeats { get; set; } = new List<FlightSeatEntity>();

    public static SeatLocationTypeEntity FromDomain(SeatLocationType seatLocationType)
    {
        return new SeatLocationTypeEntity
        {
            Id = seatLocationType.Id?.Value ?? 0,
            Name = seatLocationType.Name.Value
        };
    }

    public SeatLocationType ToDomain()
    {
        return SeatLocationType.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject.SeatLocationTypeId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject.SeatLocationTypeName.Reconstitute(Name)
        );
    }
}
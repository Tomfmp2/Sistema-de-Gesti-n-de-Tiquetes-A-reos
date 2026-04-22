using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Reservations.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatusTransitions.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Infrastructure.Entity;

public class ReservationStatusEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public ICollection<ReservationEntity> Reservations { get; set; } = new List<ReservationEntity>();
    public ICollection<ReservationStatusTransitionEntity> OriginTransitions { get; set; } =
        new List<ReservationStatusTransitionEntity>();
    public ICollection<ReservationStatusTransitionEntity> DestinationTransitions { get; set; } =
        new List<ReservationStatusTransitionEntity>();
}

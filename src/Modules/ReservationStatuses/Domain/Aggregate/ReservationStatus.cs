using sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.ReservationStatuses.Domain.Aggregate;

public class ReservationStatus
{
    public ReservationStatusId Id { get; private set; }
    public ReservationStatusName Name { get; private set; }

    private ReservationStatus(ReservationStatusId id, ReservationStatusName name)
    {
        Id = id;
        Name = name;
    }

    public static ReservationStatus Create(ReservationStatusId id, ReservationStatusName name)
    {
        return new ReservationStatus(id, name);
    }
}

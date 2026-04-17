using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Infrastructure.Entity;

public class CabinConfigurationEntity
{
    public int Id { get; set; }
    public int AircraftId { get; set; }
    public int CabinTypeId { get; set; }
    public int StartRow { get; set; }
    public int EndRow { get; set; }
    public int SeatsPerRow { get; set; }
    public string SeatLetters { get; set; } = string.Empty;

    public static CabinConfigurationEntity FromDomain(sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate.CabinConfiguration cabinConfiguration)
    {
        return new CabinConfigurationEntity
        {
            Id = cabinConfiguration.Id.Value,
            AircraftId = cabinConfiguration.AircraftId.Value,
            CabinTypeId = cabinConfiguration.CabinTypeId.Value,
            StartRow = cabinConfiguration.StartRow.Value,
            EndRow = cabinConfiguration.EndRow.Value,
            SeatsPerRow = cabinConfiguration.SeatsPerRow.Value,
            SeatLetters = cabinConfiguration.SeatLetters.Value
        };
    }

    public sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate.CabinConfiguration ToDomain()
    {
        return sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate.CabinConfiguration.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.CabinConfigurationId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.AircraftId.Reconstitute(AircraftId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.CabinTypeId.Reconstitute(CabinTypeId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.StartRow.Reconstitute(StartRow),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.EndRow.Reconstitute(EndRow),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.SeatsPerRow.Reconstitute(SeatsPerRow),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject.SeatLetters.Reconstitute(SeatLetters));
    }
}
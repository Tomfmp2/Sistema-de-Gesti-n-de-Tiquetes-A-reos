using Aggregate = sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;
using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.AircraftModels.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Flights.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Infrastructure.Entity;

public class AircraftEntity
{
    public int Id { get; set; }
    public int ModelId { get; set; }
    public int AirlineId { get; set; }
    public string Registration { get; set; } = string.Empty;
    public DateOnly? ManufacturingDate { get; set; }
    public bool IsActive { get; set; }

    // Navigation properties
    public AircraftModelEntity? Model { get; set; }
    public AirlineEntity? Airline { get; set; }
    public ICollection<FlightEntity> Flights { get; set; } = new List<FlightEntity>();

    public static AircraftEntity FromDomain(Aggregate.Aircraft aircraft)
    {
        return new AircraftEntity
        {
            Id = aircraft.Id.Value,
            ModelId = aircraft.ModelId.Value,
            AirlineId = aircraft.AirlineId.Value,
            Registration = aircraft.Registration.Value,
            ManufacturingDate = aircraft.ManufacturingDate?.Value,
            IsActive = aircraft.IsActive.Value
        };
    }

    public Aggregate.Aircraft ToDomain()
    {
        return Aggregate.Aircraft.Reconstitute(
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject.AircraftId.Reconstitute(Id),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject.ModelId.Reconstitute(ModelId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject.AirlineId.Reconstitute(AirlineId),
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject.Registration.Reconstitute(Registration),
            ManufacturingDate.HasValue ? sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject.ManufacturingDate.Reconstitute(ManufacturingDate.Value) : null,
            sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject.IsActive.Reconstitute(IsActive)
        );
    }
}

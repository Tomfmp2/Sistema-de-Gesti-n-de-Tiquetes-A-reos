using sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Aircraft.Domain.Aggregate;

public class Aircraft
{
    public AircraftId Id { get; internal set; }
    public ModelId ModelId { get; private set; }
    public AirlineId AirlineId { get; private set; }
    public Registration Registration { get; private set; }
    public ManufacturingDate? ManufacturingDate { get; private set; }
    public IsActive IsActive { get; private set; }

    private Aircraft(
        AircraftId id,
        ModelId modelId,
        AirlineId airlineId,
        Registration registration,
        ManufacturingDate? manufacturingDate,
        IsActive isActive)
    {
        Id = id;
        ModelId = modelId;
        AirlineId = airlineId;
        Registration = registration;
        ManufacturingDate = manufacturingDate;
        IsActive = isActive;
    }

    public static Aircraft Create(
        ModelId modelId,
        AirlineId airlineId,
        Registration registration,
        ManufacturingDate? manufacturingDate,
        IsActive isActive)
    {
        return new Aircraft(
            AircraftId.Create(0), // will be set by DB
            modelId,
            airlineId,
            registration,
            manufacturingDate,
            isActive);
    }

    public static Aircraft Reconstitute(
        AircraftId id,
        ModelId modelId,
        AirlineId airlineId,
        Registration registration,
        ManufacturingDate? manufacturingDate,
        IsActive isActive)
    {
        return new Aircraft(id, modelId, airlineId, registration, manufacturingDate, isActive);
    }
}
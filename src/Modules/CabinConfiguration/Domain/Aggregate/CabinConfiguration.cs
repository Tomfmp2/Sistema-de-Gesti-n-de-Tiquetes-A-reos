using sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.CabinConfiguration.Domain.Aggregate;

public sealed class CabinConfiguration
{
    public CabinConfigurationId Id { get; internal set; }
    public AircraftId AircraftId { get; private set; }
    public CabinTypeId CabinTypeId { get; private set; }
    public StartRow StartRow { get; private set; }
    public EndRow EndRow { get; private set; }
    public SeatsPerRow SeatsPerRow { get; private set; }
    public SeatLetters SeatLetters { get; private set; }

    private CabinConfiguration(
        CabinConfigurationId id,
        AircraftId aircraftId,
        CabinTypeId cabinTypeId,
        StartRow startRow,
        EndRow endRow,
        SeatsPerRow seatsPerRow,
        SeatLetters seatLetters)
    {
        Id = id;
        AircraftId = aircraftId;
        CabinTypeId = cabinTypeId;
        StartRow = startRow;
        EndRow = endRow;
        SeatsPerRow = seatsPerRow;
        SeatLetters = seatLetters;
    }

    public static CabinConfiguration Create(
        AircraftId aircraftId,
        CabinTypeId cabinTypeId,
        StartRow startRow,
        EndRow endRow,
        SeatsPerRow seatsPerRow,
        SeatLetters seatLetters)
    {
        return new CabinConfiguration(
            CabinConfigurationId.Create(0),
            aircraftId,
            cabinTypeId,
            startRow,
            endRow,
            seatsPerRow,
            seatLetters);
    }

    public static CabinConfiguration Reconstitute(
        CabinConfigurationId id,
        AircraftId aircraftId,
        CabinTypeId cabinTypeId,
        StartRow startRow,
        EndRow endRow,
        SeatsPerRow seatsPerRow,
        SeatLetters seatLetters)
    {
        return new CabinConfiguration(
            id,
            aircraftId,
            cabinTypeId,
            startRow,
            endRow,
            seatsPerRow,
            seatLetters);
    }
}
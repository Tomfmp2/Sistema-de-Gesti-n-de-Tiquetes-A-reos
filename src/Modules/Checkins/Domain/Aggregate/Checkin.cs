using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;

public class Checkin
{
    public CheckinId Id { get; private set; }
    public CheckinTicketId TicketId { get; private set; }
    public CheckinStaffId StaffId { get; private set; }
    public CheckinFlightSeatId FlightSeatId { get; private set; }
    public CheckinDate CheckinDate { get; private set; }
    public CheckinStatusId CheckinStatusId { get; private set; }
    public CheckinBoardingPassNumber BoardingPassNumber { get; private set; }
    public CheckinHasCheckedBaggage HasCheckedBaggage { get; private set; }
    public CheckinBaggageWeightKg BaggageWeightKg { get; private set; }

    private Checkin(
        CheckinId id,
        CheckinTicketId ticketId,
        CheckinStaffId staffId,
        CheckinFlightSeatId flightSeatId,
        CheckinDate checkinDate,
        CheckinStatusId checkinStatusId,
        CheckinBoardingPassNumber boardingPassNumber,
        CheckinHasCheckedBaggage hasCheckedBaggage,
        CheckinBaggageWeightKg baggageWeightKg
    )
    {
        Id = id;
        TicketId = ticketId;
        StaffId = staffId;
        FlightSeatId = flightSeatId;
        CheckinDate = checkinDate;
        CheckinStatusId = checkinStatusId;
        BoardingPassNumber = boardingPassNumber;
        HasCheckedBaggage = hasCheckedBaggage;
        BaggageWeightKg = baggageWeightKg;
    }

    public static Checkin Create(
        CheckinId id,
        CheckinTicketId ticketId,
        CheckinStaffId staffId,
        CheckinFlightSeatId flightSeatId,
        CheckinDate checkinDate,
        CheckinStatusId checkinStatusId,
        CheckinBoardingPassNumber boardingPassNumber,
        CheckinHasCheckedBaggage hasCheckedBaggage,
        CheckinBaggageWeightKg baggageWeightKg
    )
    {
        return new Checkin(
            id,
            ticketId,
            staffId,
            flightSeatId,
            checkinDate,
            checkinStatusId,
            boardingPassNumber,
            hasCheckedBaggage,
            baggageWeightKg
        );
    }
}

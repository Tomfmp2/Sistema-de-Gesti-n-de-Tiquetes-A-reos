using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;

public interface ICreateCheckinUseCase
{
    Task<Checkin> ExecuteAsync(
        CreateCheckinRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class CreateCheckinUseCase : ICreateCheckinUseCase
{
    private readonly ICheckinRepository _repository;

    public CreateCheckinUseCase(ICheckinRepository repository)
    {
        _repository = repository;
    }

    public Task<Checkin> ExecuteAsync(
        CreateCheckinRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Checkin.Create(new CheckinId(0), CheckinTicketId.Create(request.TicketId), CheckinStaffId.Create(request.StaffId), CheckinFlightSeatId.Create(request.FlightSeatId), CheckinDate.Create(request.CheckinDate), CheckinStatusId.Create(request.CheckinStatusId), CheckinBoardingPassNumber.Create(request.BoardingPassNumber), CheckinHasCheckedBaggage.Create(request.HasCheckedBaggage), CheckinBaggageWeightKg.Create(request.BaggageWeightKg));
        return _repository.AddAsync(x, cancellationToken);
    }
}

using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.Dtos;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Domain.ValueObjet;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Checkins.Application.UseCases;

public interface IUpdateCheckinUseCase
{
    Task ExecuteAsync(
        UpdateCheckinRequest request,
        CancellationToken cancellationToken = default
    );
}

public sealed class UpdateCheckinUseCase : IUpdateCheckinUseCase
{
    private readonly ICheckinRepository _repository;

    public UpdateCheckinUseCase(ICheckinRepository repository)
    {
        _repository = repository;
    }

    public Task ExecuteAsync(
        UpdateCheckinRequest request,
        CancellationToken cancellationToken = default
    )
    {
        var x = Checkin.Create(CheckinId.Create(request.Id), CheckinTicketId.Create(request.TicketId), CheckinStaffId.Create(request.StaffId), CheckinFlightSeatId.Create(request.FlightSeatId), CheckinDate.Create(request.CheckinDate), CheckinStatusId.Create(request.CheckinStatusId), CheckinBoardingPassNumber.Create(request.BoardingPassNumber), CheckinHasCheckedBaggage.Create(request.HasCheckedBaggage), CheckinBaggageWeightKg.Create(request.BaggageWeightKg));
        return _repository.UpdateAsync(x, cancellationToken);
    }
}

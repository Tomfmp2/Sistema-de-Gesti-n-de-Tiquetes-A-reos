using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;

public class UpdateSeatLocationTypeUseCase
{
    private readonly ISeatLocationTypeRepository _repository;

    public UpdateSeatLocationTypeUseCase(ISeatLocationTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SeatLocationTypeId id, SeatLocationTypeName name)
    {
        var seatLocationType = SeatLocationType.Reconstitute(id, name);
        await _repository.UpdateAsync(seatLocationType);
    }
}
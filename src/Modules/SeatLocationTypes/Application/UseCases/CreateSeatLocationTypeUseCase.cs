using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;

public class CreateSeatLocationTypeUseCase
{
    private readonly ISeatLocationTypeRepository _repository;

    public CreateSeatLocationTypeUseCase(ISeatLocationTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SeatLocationTypeName name)
    {
        var seatLocationType = SeatLocationType.Create(name);
        await _repository.AddAsync(seatLocationType);
    }
}
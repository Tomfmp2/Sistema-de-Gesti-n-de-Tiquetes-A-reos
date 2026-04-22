using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;

public class GetSeatLocationTypeByIdUseCase
{
    private readonly ISeatLocationTypeRepository _repository;

    public GetSeatLocationTypeByIdUseCase(ISeatLocationTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<SeatLocationType?> ExecuteAsync(SeatLocationTypeId id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
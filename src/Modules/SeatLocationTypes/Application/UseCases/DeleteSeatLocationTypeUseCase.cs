using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;

public class DeleteSeatLocationTypeUseCase
{
    private readonly ISeatLocationTypeRepository _repository;

    public DeleteSeatLocationTypeUseCase(ISeatLocationTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(SeatLocationTypeId id)
    {
        await _repository.DeleteAsync(id);
    }
}
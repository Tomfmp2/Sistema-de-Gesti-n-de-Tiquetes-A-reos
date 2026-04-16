using System.Collections.Generic;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Application.UseCases;

public class GetAllSeatLocationTypesUseCase
{
    private readonly ISeatLocationTypeRepository _repository;

    public GetAllSeatLocationTypesUseCase(ISeatLocationTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SeatLocationType>> ExecuteAsync()
    {
        return await _repository.GetAllAsync();
    }
}
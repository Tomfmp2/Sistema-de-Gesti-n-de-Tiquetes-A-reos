using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.SeatLocationTypes.Infrastructure.repository;

public class SeatLocationTypeRepository : ISeatLocationTypeRepository
{
    private readonly AppDbContext _context;

    public SeatLocationTypeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SeatLocationType> GetByIdAsync(SeatLocationTypeId id)
    {
        var entity = await _context.SeatLocationTypes.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<SeatLocationType>> GetAllAsync()
    {
        var entities = await _context.SeatLocationTypes.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(SeatLocationType seatLocationType)
    {
        var entity = SeatLocationTypeEntity.FromDomain(seatLocationType);
        _context.SeatLocationTypes.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SeatLocationType seatLocationType)
    {
        var entity = SeatLocationTypeEntity.FromDomain(seatLocationType);
        _context.SeatLocationTypes.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SeatLocationTypeId id)
    {
        var entity = await _context.SeatLocationTypes.FindAsync(id.Value);
        if (entity != null)
        {
            _context.SeatLocationTypes.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
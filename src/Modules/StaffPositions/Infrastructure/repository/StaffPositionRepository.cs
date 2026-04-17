using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Repository;

public class StaffPositionRepository : IStaffPositionRepository
{
    private readonly AppDbContext _context;

    public StaffPositionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StaffPosition?> GetByIdAsync(StaffPositionId id)
    {
        var entity = await _context.StaffPositions.FindAsync(id.Value);
        if (entity == null) return null;
        return StaffPosition.Reconstitute(
            StaffPositionId.Reconstitute(entity.Id),
            StaffPositionName.Reconstitute(entity.Name)
        );
    }

    public async Task<IEnumerable<StaffPosition>> GetAllAsync()
    {
        var entities = await _context.StaffPositions.ToListAsync();
        return entities.Select(e => StaffPosition.Reconstitute(
            StaffPositionId.Reconstitute(e.Id),
            StaffPositionName.Reconstitute(e.Name)
        ));
    }

    public async Task AddAsync(StaffPosition staffPosition)
    {
        var entity = new StaffPositionEntity
        {
            Id = staffPosition.Id.Value,
            Name = staffPosition.Name.Value
        };
        await _context.StaffPositions.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StaffPosition staffPosition)
    {
        var entity = await _context.StaffPositions.FindAsync(staffPosition.Id.Value);
        if (entity != null)
        {
            entity.Name = staffPosition.Name.Value;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(StaffPositionId id)
    {
        var entity = await _context.StaffPositions.FindAsync(id.Value);
        if (entity != null)
        {
            _context.StaffPositions.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
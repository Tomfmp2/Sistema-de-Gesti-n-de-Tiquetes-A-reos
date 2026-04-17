using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Staff.Infrastructure.Repository;

public class StaffRepository : IStaffRepository
{
    private readonly AppDbContext _context;

    public StaffRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StaffRecord?> GetByIdAsync(StaffId id)
    {
        var entity = await _context.Staff.FindAsync(id.Value);
        if (entity == null) return null;
        return StaffRecord.Reconstitute(
            StaffId.Reconstitute(entity.Id),
            PersonId.Reconstitute(entity.PersonId),
            PositionId.Reconstitute(entity.PositionId),
            entity.AirlineId.HasValue ? AirlineId.Reconstitute(entity.AirlineId.Value) : null,
            entity.AirportId.HasValue ? AirportId.Reconstitute(entity.AirportId.Value) : null,
            HireDate.Reconstitute(entity.HireDate),
            IsActive.Reconstitute(entity.IsActive),
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    public async Task<IEnumerable<StaffRecord>> GetAllAsync()
    {
        var entities = await _context.Staff.ToListAsync();
        return entities.Select(e => StaffRecord.Reconstitute(
            StaffId.Reconstitute(e.Id),
            PersonId.Reconstitute(e.PersonId),
            PositionId.Reconstitute(e.PositionId),
            e.AirlineId.HasValue ? AirlineId.Reconstitute(e.AirlineId.Value) : null,
            e.AirportId.HasValue ? AirportId.Reconstitute(e.AirportId.Value) : null,
            HireDate.Reconstitute(e.HireDate),
            IsActive.Reconstitute(e.IsActive),
            e.CreatedAt,
            e.UpdatedAt
        ));
    }

    public async Task AddAsync(StaffRecord staff)
    {
        var entity = new StaffEntity
        {
            Id = staff.Id.Value,
            PersonId = staff.PersonId.Value,
            PositionId = staff.PositionId.Value,
            AirlineId = staff.AirlineId?.Value,
            AirportId = staff.AirportId?.Value,
            HireDate = staff.HireDate.Value,
            IsActive = staff.IsActive.Value,
            CreatedAt = staff.CreatedAt,
            UpdatedAt = staff.UpdatedAt
        };
        await _context.Staff.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(StaffRecord staff)
    {
        var entity = await _context.Staff.FindAsync(staff.Id.Value);
        if (entity != null)
        {
            entity.PersonId = staff.PersonId.Value;
            entity.PositionId = staff.PositionId.Value;
            entity.AirlineId = staff.AirlineId?.Value;
            entity.AirportId = staff.AirportId?.Value;
            entity.HireDate = staff.HireDate.Value;
            entity.IsActive = staff.IsActive.Value;
            entity.UpdatedAt = staff.UpdatedAt;
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(StaffId id)
    {
        var entity = await _context.Staff.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Staff.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
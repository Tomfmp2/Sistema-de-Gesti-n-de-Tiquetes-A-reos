using Microsoft.EntityFrameworkCore;
using AppFlightAssign = sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Application.Interfaces;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.Aggregate;
using DomFlightAssign = sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.Entity;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.FlightAssignments.Infrastructure.repository;

public sealed class FlightAssignmentRepository
    : DomFlightAssign.IFlightAssignmentRepository,
        AppFlightAssign.IFlightAssignmentRepository
{
    private readonly DbContext _context;
    private readonly DbSet<FlightAssignmentEntity> _dbSet;

    public FlightAssignmentRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<FlightAssignmentEntity>();
    }

    public async Task<FlightAssignment?> GetByIdAsync(FlightAssignmentId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        
        if (entity == null)
            return null;

        return MapToDomain(entity);
    }

    public async Task<IEnumerable<FlightAssignment>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _dbSet.ToListAsync(cancellationToken);
        return entities.Select(MapToDomain).ToList();
    }

    public async Task<FlightAssignment> AddAsync(FlightAssignment flightAssignment, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(flightAssignment);
        await _dbSet.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return FlightAssignment.Reconstitute(
            new FlightAssignmentId(entity.Id),
            flightAssignment.FlightId,
            flightAssignment.StaffId,
            flightAssignment.FlightRoleId);
    }

    public async Task<FlightAssignment> UpdateAsync(FlightAssignment flightAssignment, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(flightAssignment);
        _dbSet.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return flightAssignment;
    }

    public async Task<bool> DeleteAsync(FlightAssignmentId id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);
        
        if (entity == null)
            return false;

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static FlightAssignment MapToDomain(FlightAssignmentEntity entity)
    {
        return FlightAssignment.Reconstitute(
            new FlightAssignmentId(entity.Id),
            new FlightId(entity.FlightId),
            new StaffId(entity.StaffId),
            new FlightRoleId(entity.FlightRoleId));
    }

    private static FlightAssignmentEntity MapToEntity(FlightAssignment flightAssignment)
    {
        return new FlightAssignmentEntity
        {
            Id = flightAssignment.Id.Value,
            FlightId = flightAssignment.FlightId.Value,
            StaffId = flightAssignment.StaffId.Value,
            FlightRoleId = flightAssignment.FlightRoleId.Value
        };
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AppSeasons = sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Application.Interfaces;
using DomSeasons = sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Seasons.Infrastructure.repository;

public class SeasonRepository : DomSeasons.ISeasonRepository, AppSeasons.ISeasonRepository
{
    private readonly AppDbContext _context;

    public SeasonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Season> GetByIdAsync(SeasonId id)
    {
        var entity = await _context.Seasons.FindAsync(id.Value);
        return entity?.ToDomain();
    }

    public async Task<IEnumerable<Season>> GetAllAsync()
    {
        var entities = await _context.Seasons.ToListAsync();
        return entities.Select(e => e.ToDomain());
    }

    public async Task AddAsync(Season season)
    {
        var entity = SeasonEntity.FromDomain(season);
        _context.Seasons.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Season season)
    {
        var entity = SeasonEntity.FromDomain(season);
        _context.Seasons.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SeasonId id)
    {
        var entity = await _context.Seasons.FindAsync(id.Value);
        if (entity != null)
        {
            _context.Seasons.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
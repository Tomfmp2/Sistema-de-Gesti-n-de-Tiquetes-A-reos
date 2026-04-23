using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Aggregate;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.Repositories;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Domain.ValueObject;
using sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.Entity;
using sistema_gestor_de_tiquetes_aereos.Src.Shared.Context;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.Airlines.Infrastructure.repository;

public class AirlineRepository : IAirlineRepository
{
    private readonly AppDbContext _context;

    public AirlineRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Airline?> GetByIdAsync(AirlineId id)
    {
        var entity = await _context.Airlines.FindAsync(id.Value);
        if (entity == null) return null;
        return Airline.Reconstitute(
            AirlineId.Reconstitute(entity.Id),
            AirlineName.Reconstitute(entity.Name),
            IataCode.Reconstitute(entity.IataCode),
            entity.OriginCountryId,
            entity.IsActive,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    public async Task<IEnumerable<Airline>> GetAllAsync()
    {
        var entities = await _context.Airlines.Where(x => x.IsActive).ToListAsync();
        return entities.Select(e => Airline.Reconstitute(
            AirlineId.Reconstitute(e.Id),
            AirlineName.Reconstitute(e.Name),
            IataCode.Reconstitute(e.IataCode),
            e.OriginCountryId,
            e.IsActive,
            e.CreatedAt,
            e.UpdatedAt
        ));
    }

    public async Task AddAsync(Airline airline)
    {
        var countryExists = await _context.Countries.AnyAsync(c => c.Id == airline.OriginCountryId);
        if (!countryExists)
        {
            throw new InvalidOperationException(
                $"No existe un país con id={airline.OriginCountryId}. Use un id válido de la tabla countries (listar países en la base o cargar datos de catálogo)."
            );
        }

        // Evitar duplicados por nombre o IATA (independiente de IsActive).
        var name = (airline.Name.Value ?? string.Empty).Trim();
        var iata = (airline.IataCode.Value ?? string.Empty).Trim();
        var duplicateExists = await _context.Airlines
            .AsNoTracking()
            .AnyAsync(a =>
                (a.Name != null && a.Name.Trim().ToUpper() == name.ToUpper())
                || (a.IataCode != null && a.IataCode.Trim().ToUpper() == iata.ToUpper())
            );
        if (duplicateExists)
        {
            throw new InvalidOperationException(
                $"Ya existe una aerolínea con el mismo nombre ('{name}') o el mismo código IATA ('{iata}')."
            );
        }

        var entity = new AirlineEntity
        {
            Name = airline.Name.Value ?? string.Empty,
            IataCode = airline.IataCode.Value ?? string.Empty,
            OriginCountryId = airline.OriginCountryId,
            IsActive = airline.IsActive,
            CreatedAt = airline.CreatedAt,
            UpdatedAt = airline.UpdatedAt
        };
        _context.Airlines.Add(entity);
        await _context.SaveChangesAsync();
        // set id back
        airline.Id = AirlineId.Reconstitute(entity.Id);
    }

    public async Task UpdateAsync(Airline airline)
    {
        var countryExists = await _context.Countries.AnyAsync(c => c.Id == airline.OriginCountryId);
        if (!countryExists)
        {
            throw new InvalidOperationException(
                $"No existe un país con id={airline.OriginCountryId}. Use un id válido de la tabla countries."
            );
        }

        var entity = await _context.Airlines.FindAsync(airline.Id.Value);
        if (entity == null) return;
        entity.Name = airline.Name.Value;
        entity.IataCode = airline.IataCode.Value;
        entity.OriginCountryId = airline.OriginCountryId;
        entity.IsActive = airline.IsActive;
        entity.UpdatedAt = airline.UpdatedAt;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(AirlineId id)
    {
        var entity = await _context.Airlines.FindAsync(id.Value);
        if (entity != null)
        {
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }
    }
}
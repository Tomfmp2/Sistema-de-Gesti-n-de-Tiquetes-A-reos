using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace sistema_gestor_de_tiquetes_aereos.Src.Modules.StaffPositions.Infrastructure.Entity;

[Table("staff_positions")]
public class StaffPositionEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace sistema_gestor_de_tiquetes_aereos.Src.Shared.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}

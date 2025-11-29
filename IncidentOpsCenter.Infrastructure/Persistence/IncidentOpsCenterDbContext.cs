using IncidentOpsCenter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace IncidentOpsCenter.Infrastructure.Persistence
{
    /// <summary>
    /// DbContext principal de la aplicación.
    /// Por ahora lo usamos con InMemory, después podemos pasar a SQL Server.
    /// </summary>
    public class IncidentOpsCenterDbContext : DbContext
    {
        public IncidentOpsCenterDbContext(
            DbContextOptions<IncidentOpsCenterDbContext> options)
            : base(options)
        {
        }

        // Representa la tabla de Incidents
        public DbSet<Incident> Incidents => Set<Incident>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.ToTable("Incidents");

                entity.HasKey(i => i.Id);

                entity.Property(i => i.IncidentNumber)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(i => i.Title)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(i => i.ServiceName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(i => i.Environment)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(i => i.ReportedBy)
                      .IsRequired()
                      .HasMaxLength(100);
            });
        }
    }
}

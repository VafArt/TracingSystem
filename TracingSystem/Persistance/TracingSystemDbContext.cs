using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Domain;

namespace TracingSystem.Persistance
{
    public class TracingSystemDbContext : DbContext, ITracingSystemDbContext
    {
        public DbSet<Layer> Layers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Pcb> Pcbs { get; set; }

        public DbSet<Trace> Traces { get; set; }

        public DbSet<Element> Elements { get; set; }

        public DbSet<ElementPoint> ElementPoints { get; set; }

        public DbSet<TracePoint> TracePoints { get; set; }

        public TracingSystemDbContext(DbContextOptions<TracingSystemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Project>()
                .HasMany(project => project.Pcbs)
                .WithOne(pcb => pcb.Project)
                .HasForeignKey(pcb => pcb.ProjectId);

            modelBuilder.Entity<Pcb>()
                .HasMany(pcb => pcb.Layers)
                .WithOne(layer => layer.Pcb)
                .HasForeignKey(layer => layer.PcbId);

            modelBuilder.Entity<Layer>()
                .HasMany(layer => layer.Traces)
                .WithOne(trace => trace.Layer)
                .HasForeignKey(trace => trace.LayerId);

            modelBuilder.Entity<Layer>()
                .HasMany(layer => layer.Elements)
                .WithOne(element => element.Layer)
                .HasForeignKey(element => element.LayerId);

            modelBuilder.Entity<Trace>()
                .HasMany(trace => trace.DirectionChangingCoords)
                .WithOne(directionChangingCoord => directionChangingCoord.Trace)
                .HasForeignKey(directionChangingCoord => directionChangingCoord.TraceId);

            modelBuilder.Entity<Element>()
                .HasMany(element => element.PadsCoords)
                .WithOne(padsCoords => padsCoords.Element)
                .HasForeignKey(padsCoords => padsCoords.ElementId);
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
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

            modelBuilder.Entity<Element>()
                .HasOne(element => element.Location)
                .WithOne(elementPoint => elementPoint.Element)
                .HasForeignKey(typeof(ElementPoint), "ElementId");
        }
    }
}

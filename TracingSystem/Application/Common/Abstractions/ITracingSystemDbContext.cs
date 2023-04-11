using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Domain;

namespace TracingSystem.Application.Common.Abstractions
{
    public interface ITracingSystemDbContext
    {
        public DbSet<Layer> Layers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Pcb> Pcbs { get; set; }

        public DbSet<Trace> Traces { get; set; }

        public DbSet<Element> Elements { get; set; }

        public ChangeTracker ChangeTracker { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}

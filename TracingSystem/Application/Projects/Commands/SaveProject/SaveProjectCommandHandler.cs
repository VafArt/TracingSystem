using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Application.Projects.Commands.CreateProject;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;
using TracingSystem.Persistance;

namespace TracingSystem.Application.Projects.Commands.SaveProject
{
    public sealed class SaveProjectCommandHandler : ICommandHandler<SaveProjectCommand>
    {
        private readonly ITracingSystemDbContext _dbContext;

        public SaveProjectCommandHandler(ITracingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result> Handle(SaveProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.FirstOrDefaultAsync(project => project.Id == request.Project.Id, cancellationToken);
            if (project == null) return Result.Failure(DomainErrors.Project.ProjectNotFound);

            foreach(var pcb in request.Project.Pcbs)
            {
                foreach(var element in pcb.Layers.SelectMany(layer=>layer.Elements))
                {
                    element.LocationX = element.ElementControl.Location.X;
                    element.LocationY = element.ElementControl.Location.Y;
                }
            }

            project.PcbLib = request.Project.PcbLib;
            project.Name = request.Project.Name;
            project.State = request.Project.State;
            project.Pcbs = request.Project.Pcbs;
            await _dbContext.SaveChangesAsync(cancellationToken);
            _dbContext.ChangeTracker.Clear();
            return Result.Success();
        }
    }
}

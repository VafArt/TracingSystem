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
            var project = await _dbContext.Projects.AsNoTracking().SingleOrDefaultAsync(project => project.Id == request.Project.Id);
            if (project == null) return Result.Failure(DomainErrors.Project.ProjectNotFound);

            project.PcbLib = request.Project.PcbLib;
            project.Name = request.Project.Name;
            project.State = request.Project.State;
            project.Pcbs = request.Project.Pcbs;
            _dbContext.ChangeTracker.DetectChanges();
            await _dbContext.SaveChangesAsync(cancellationToken);
            _dbContext.ChangeTracker.Clear();
            return Result.Success();
        }
    }
}

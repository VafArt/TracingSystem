using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Application.Services;
using TracingSystem.Domain;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Projects.Commands.DeleteProjectByName
{
    public sealed class DeleteProjectByNameCommandHandler : ICommandHandler<DeleteProjectByNameCommand>
    {
        private readonly ITracingSystemDbContext _dbContext;

        private readonly IProjectDataService _project;

        public DeleteProjectByNameCommandHandler(ITracingSystemDbContext dbContext, IProjectDataService project)
        {
            _dbContext = dbContext;
            _project = project;
        }

        public async Task<Result> Handle(DeleteProjectByNameCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                .SingleOrDefaultAsync(project => project.Name == request.ProjectName);
            if (project == null) return Result.Failure(project, DomainErrors.Project.ProjectNotFound);

            _dbContext.Projects.Remove(project);

            if(_project.Name == project.Name)
            {
                _project.Name = string.Empty;
                _project.Project = null;
                _project.State = ProjectState.Startup;
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}

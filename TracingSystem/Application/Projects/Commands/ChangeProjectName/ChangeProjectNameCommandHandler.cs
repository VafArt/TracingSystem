using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Application.Services;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Projects.Commands.UpdateProject
{
    public sealed class ChangeProjectNameCommandHandler : ICommandHandler<ChangeProjectNameCommand>
    {
        private readonly ITracingSystemDbContext _dbContext;
        private readonly IProjectDataService _project;

        public ChangeProjectNameCommandHandler(ITracingSystemDbContext dbContext, IProjectDataService project)
        {
            _dbContext = dbContext;
            _project = project;
        }

        public async Task<Result> Handle(ChangeProjectNameCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Name == _project.Name);
            if (project == null) return Result.Failure(DomainErrors.Project.ProjectNotFound);
            project.Name = request.ProjectName;
            _project.Name = request.ProjectName;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}

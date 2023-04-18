using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Domain;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Projects.Queries.GetProjectByName
{
    public sealed class GetProjectByNameQueryHandler : IQueryHandler<GetProjectByNameQuery, Project>
    {
        private readonly ITracingSystemDbContext _dbContext;

        public GetProjectByNameQueryHandler(ITracingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Project>> Handle(GetProjectByNameQuery request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects
                .Include(project => project.Pcbs)
                .FirstOrDefaultAsync(project => project.Name == request.Name);
            if (project == null) return Result.Failure(project, DomainErrors.Project.ProjectNotFound);

            return project;
        }
    }
}

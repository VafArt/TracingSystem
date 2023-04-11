using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Domain.Errors;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Projects.Queries.GetProjectIdByName
{
    public sealed class GetProjectIdByNameQueryHandler : IQueryHandler<GetProjectIdByNameQuery, int>
    {
        private readonly ITracingSystemDbContext _context;

        public GetProjectIdByNameQueryHandler(ITracingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Result<int>> Handle(GetProjectIdByNameQuery request, CancellationToken cancellationToken)
        {
            var projectId = await _context.Projects
                .Where(project => project.Name == request.ProjectName)
                .Select(project => project.Id)
                .FirstOrDefaultAsync();
            if (projectId == 0) return Result.Failure(projectId, DomainErrors.Project.ProjectNotFound);

            return projectId;
        }
    }
}

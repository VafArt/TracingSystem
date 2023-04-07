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

namespace TracingSystem.Application.Projects.Queries.GetAllProjectNames
{
    public sealed class GetAllProjectNamesQueryHandler : IQueryHandler<GetAllProjectNamesQuery, List<string>>
    {
        private readonly ITracingSystemDbContext _dbContext;

        public GetAllProjectNamesQueryHandler(ITracingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<string>>> Handle(GetAllProjectNamesQuery request, CancellationToken cancellationToken)
        {
            var projectsNames = await _dbContext.Projects.Select(x => x.Name).ToListAsync();

            if (projectsNames == null) return Result.Failure(projectsNames, DomainErrors.Project.ProjectTableIsEmpty);
            return projectsNames;
        }
    }
}

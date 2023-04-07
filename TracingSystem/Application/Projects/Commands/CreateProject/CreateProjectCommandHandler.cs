﻿using MediatR;
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

namespace TracingSystem.Application.Projects.Commands.CreateProject
{
    public sealed class CreateProjectCommandHandler : ICommandHandler<CreateProjectCommand, Project>
    {
        private readonly ITracingSystemDbContext _dbContext;

        public CreateProjectCommandHandler(ITracingSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Project>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _dbContext.Projects.SingleOrDefaultAsync(project => project.Name == request.Name);
            if (project != null) return Result.Failure(project, DomainErrors.Project.AlreadyHas);

            project = new Project()
            {
                Name = request.Name,
            };
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return project;
        }
    }
}
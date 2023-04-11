using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TracingSystem.Application.Common.Abstractions;
using TracingSystem.Application.Common.Abstractions.CQRS;
using TracingSystem.Domain;
using TracingSystem.Domain.Shared;

namespace TracingSystem.Application.Projects.Commands.DeleteProject
{
    public sealed class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
    {
        private readonly ITracingSystemDbContext _context;

        public DeleteProjectCommandHandler(ITracingSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Project() { Id = request.ProjectId };
            _context.Projects.Attach(project);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
